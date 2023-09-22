package main

import (
	"fmt"
	"time"
)

func main() {
	current := time.Now()
	banTime := current.Add(1 * time.Minute)
	//new := current.Add(10 * time.Minute)
	<-time.NewTimer(time.Minute + time.Second).C
	if time.Now().After(banTime) {
		fmt.Println("got")
	} else {
		fmt.Println("not")
	}
}
