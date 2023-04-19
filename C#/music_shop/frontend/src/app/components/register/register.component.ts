import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup
  isSame: boolean = false
  constructor(private as: AuthService, private router: Router){}
  ngOnInit() {
    this.form = new FormGroup(
      {
      email: new FormControl('',
        [Validators.email, Validators.required]),
      password: new FormControl(null,
        [Validators.minLength(6), Validators.required])
              })
              }
  submit(email: string, password: string) {
    this.as.register(email, password).subscribe(
      res => { alert('REGISTERED'); this.router.navigate(['']);  },
      err => { alert('Already exists'); })
  }
}
