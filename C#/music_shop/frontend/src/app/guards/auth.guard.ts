import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { AuthService } from 'app/services/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private as: AuthService, private router: Router) {
  }

  canActivate(): boolean {
    if(!this.as.isAuthenticated()){
      this.router.navigate([''])
    }
    return true
  }

}
