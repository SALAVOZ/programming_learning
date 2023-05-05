package main

import (
	"bufio"
	"errors"
	"fmt"
	syslog "github.com/RackSec/srslog"
	"io"
	"log"
	"os"
	"path/filepath"
	"strconv"
)

// syslog "github.com/RackSec/srslog"

func os_args() {
	var str string
	arguments := os.Args
	if len(arguments) == 1 {
		str = "Give my argument"
	} else {
		str = arguments[1]
	}
	fmt.Println(str)

}

func stdin() {
	var f *os.File
	f = os.Stdin
	defer f.Close()

	scanner := bufio.NewScanner(f)
	for scanner.Scan() {
		fmt.Println(">", scanner.Text())
	}
}

func cla() {
	if len(os.Args) == 1 {
		fmt.Println("give args, at least, one")
		os.Exit(1)
	}
	arguments := os.Args
	min, _ := strconv.ParseFloat(arguments[1], 64)
	max := min
	for i := 2; i < len(arguments); i++ {
		n, _ := strconv.ParseFloat(arguments[i], 64)
		if n < min {
			min = n
		}
		if n > max {
			max = n
		}
	}
	fmt.Println("Min: ", min)
	fmt.Println("Max: ", max)
}

func errors_func() {
	if len(os.Args) == 1 {
		fmt.Println("give args, at least, one")
		os.Exit(1)
	}
	arguments := os.Args
	io.WriteString(os.Stdout, "stdout output\n")
	io.WriteString(os.Stderr, arguments[1])
	io.WriteString(os.Stderr, "\nstderr output")
}

func log_func() {
	programName := filepath.Base(os.Args[0])
	sysLog, err := syslog.New(syslog.LOG_INFO|syslog.LOG_LOCAL7, programName)

	if err != nil {
		log.Fatal(err)
	} else {
		log.SetOutput(sysLog)
	}

	log.Println("Log main: Logging in Go!")
	fmt.Println("Will you see this?")
}

func returnError(a int, b int) error {
	if a == b {
		err := errors.New("Error in returnError func")
		return err
	} else {
		return nil
	}
}

func main() {
	fmt.Println("Start")

}
