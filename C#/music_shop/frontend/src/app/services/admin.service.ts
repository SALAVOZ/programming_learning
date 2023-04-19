import {Inject, Injectable, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SHOP_IP_URL} from "../app-injection-tokens";
import {Observable} from "rxjs";
import {UserModelAdminPage} from "../models/userModelAdminPage";

@Injectable({
  providedIn: 'root'
})
export class AdminService{
  private baseUrl = `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(SHOP_IP_URL) private apiUrl: string) { }

  getIndex() {
    return this.http.get(`${this.baseUrl}admin`)
  }

  getUsers(): Observable<UserModelAdminPage[]> {
    return this.http.get<UserModelAdminPage[]>(`${this.baseUrl}admin/users`)
  }
  getRoles(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}admin/roles`)
  }
  changeRole(userId: string, role: string) {
    return this.http.post(`${this.baseUrl}admin/change`, {
      "userId": userId,
      "role": role
    })
  }

}
