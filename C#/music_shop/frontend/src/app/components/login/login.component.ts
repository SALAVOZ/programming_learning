import { Component } from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private as: AuthService, private router: Router) { }

  login(email: string, password: string) {
    this.as.login(email, password).subscribe
    (
      res => { alert('logged'); this.router.navigate(['home']); },
      error => { alert('nonono') }
    )
  }


}
