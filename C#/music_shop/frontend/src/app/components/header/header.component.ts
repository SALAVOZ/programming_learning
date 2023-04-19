import { Component, HostBinding } from '@angular/core';
import {trigger, state, style, animate, transition} from '@angular/animations';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-header',
  animations: [
    trigger('openClose', [
      state('open', style({
        transform: 'scaleY(100%) translateY(0)',
        opacity: 1,
      })),

      state('closed', style({
        transform: 'scaleY(0) translateY(-20px)',
        opacity: 0,
      })),

      transition('open => closed', [
        animate('200ms ease-in-out'),
      ]),

      transition('closed => open', [
        animate('200ms ease-in-out')
      ]),
    ])
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  //isOpen = false;

  //toggle() {
    //this.isOpen = !this.isOpen;
  //}

  constructor(private as: AuthService) { }
  isAuth(): boolean {
    return this.as.isAuthenticated()
  }
  logout() {
    this.as.logout()
  }
}
//<span class="login-menu" href="/">Регистрация</span>
/*<section [@openClose]="isOpen? 'open' : 'closed'">
  <h1>
    Вход
  </h1>
  <mat-form-field>
  <input matInput type="email" name="email" placeholder="Email" autocomplete="on"/>
  </mat-form-field>
  <mat-form-field>
  <input matInput type="password" name="password" placeholder="Пароль" autocomplete="on" />
  </mat-form-field>
  <button mat-raised-button>Войти</button>
<br><br>

  <h1>Ещё нет аккаунта?</h1>
  <mat-form-field>
  <input matInput type="email" name="email" placeholder="Email" autocomplete="on"/>
  </mat-form-field>
  <button mat-raised-button>Подтвердить Email</button>

</section>*/
