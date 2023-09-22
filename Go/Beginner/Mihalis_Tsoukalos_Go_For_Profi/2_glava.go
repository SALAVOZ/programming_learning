package main

import "C"
import (
	"fmt"
)

func d1() {
	for i := 3; i > 0; i-- {
		defer fmt.Print(i, " ")
	}
}

func d2() {
	for i := 3; i > 0; i-- {
		defer func() {
			fmt.Print(i, " ")
		}()
	}
}

func d3() {
	for i := 3; i > 0; i-- {
		defer func(n int) {
			fmt.Print(n, " ")
		}(i)
	}
}

func panic_func() {
	fmt.Println("Start")
	panic("ERROR")
	fmt.Println("Ended")
}

func recover_func_a() {
	fmt.Println("Inside recover_func")
	defer func() {
		if c := recover(); c != nil {
			fmt.Println("Recover in recover_func")
		}
	}()
	fmt.Println("About to call b()")
	recover_func_b()
	fmt.Println("recover_func_b exited")
	fmt.Println("Exiting recover_func_a()")
}

func recover_func_b() {
	fmt.Println("Inside recover_func_b")
	panic("Panic in recover_func_b()")
	fmt.Println("Exiting b()")
}
