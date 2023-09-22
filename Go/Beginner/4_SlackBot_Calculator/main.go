package main

import (
	"fmt"
	"strings"
)

func main() {
	url := "https://192.168.146.128:81/"
	url = strings.Trim(url, "/")
	url = strings.Trim(url, "http://")
	url = strings.Trim(url, "https://")

	fmt.Print(url)
}
