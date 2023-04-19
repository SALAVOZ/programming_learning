import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css']
})
export class ForgotComponent implements OnInit {

  constructor(private as: AuthService) { }

  ngOnInit(): void {
  }
  sendMessage(email:string) {
    this.as.forgotPassword(email).subscribe(
      res => {alert('Проверьте вашу почту'); },
      err => {alert('Такого пользователя не существует');}
    )
  }
}
