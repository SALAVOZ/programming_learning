package main

import (
	"fmt"
	"html/template"
	"log"
	"os"
)

type NoteTmpl struct {
	Title       string
	Description string
}

const tmplSingle = `Note - Title {{.Title}}, Description: {{.Description}}`
const tmplRange = `Notes are:
{{range .}}Title: {{.Title}}, Description: {{.Description}}
{{end}}
`
const tmpVar = `{{define "T"}}Hello {{.}}!{{end}}`

func templatesFunc() {
	note := NoteTmpl{"i love salavat", "I love rustam"}

	t := template.New("note")

	t, err := t.Parse(tmplSingle)
	if err != nil {
		log.Fatal("Parse: ", err)
		return
	}
	if err := t.Execute(os.Stdout, note); err != nil {
		log.Fatal("Execute: ", err)
		return
	}
	notes := []NoteTmpl{
		{"i love salavat", "I love salavat description"},
		{"i love rustam", "i love rustam description"},
	}
	fmt.Println("")
	t = template.New("notes")
	t, err = t.Parse(tmplRange)
	if err != nil {
		log.Fatal("Parse: ", err)
		return
	}
	if err = t.Execute(os.Stdout, notes); err != nil {
		log.Fatal("Execute: ", err)
		return
	}
	t = template.New("test")
	t, err = t.Parse(tmpVar)
	if err = t.ExecuteTemplate(os.Stdout, "T", "<script>alert('1');</script>"); err != nil {
		log.Fatal("Execute: ", err)
		return
	}
}
