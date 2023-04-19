import {Inject, Injectable} from '@angular/core';
import {Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Token } from '../models/token';
import {Router} from "@angular/router";
//import {JwtHelperService} from "@auth0/angular-jwt";
import { AUTH_IP_URL } from "../app-injection-tokens";

export const ACCESS_TOKEN_KEY = 'shop_access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(AUTH_IP_URL) private apiUrl: string,
    private router: Router
  ) { }

  login(email: string, password: string): Observable<Token>{
    return this.http.post<Token>(`${this.apiUrl}api/auth/login`,
     { email, password }).pipe(
       tap(token =>
    {
      localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
    })
    )
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY)
    if(token != null)
      return true;
    else
      return false;
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }
  register(email: string, password: string) {
    return this.http.post<void>(`${this.apiUrl}api/register`,
      {
              "email": email,
              "password": password
            }
            )
  }
  forgotPassword(email: string) {
    return this.http.get(`${this.apiUrl}api/auth/forgot/` + email)
  }
}
