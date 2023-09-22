package main

import (
	"crypto/tls"
	"fmt"
	"github.com/PuerkitoBio/goquery"
	"github.com/gophish/gophish/dialer"
	"net/http"
)

type cloneRequest struct {
	URL              string `json:"url"`
	IncludeResources bool   `json:"include_resources"`
}

func main() {
	restrictedDialer := dialer.Dialer()
	tr := &http.Transport{
		DialContext: restrictedDialer.DialContext,
		TLSClientConfig: &tls.Config{
			InsecureSkipVerify: true,
		},
	}
	client := &http.Client{Transport: tr}
	resp, err := client.Get("http://192.168.146.128:81/")
	if err != nil {
		panic("sex")
	}
	d, err := goquery.NewDocumentFromResponse(resp)
	h, err := d.Html()
	if err != nil {
		return
	}
	fmt.Println(h)

}
