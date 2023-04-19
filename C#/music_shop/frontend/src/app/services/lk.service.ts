import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SHOP_IP_URL} from "../app-injection-tokens";
import {Observable} from "rxjs";
import {lkAlbom} from "../models/lkAlbom";

@Injectable({
  providedIn: 'root'
})
export class LkService {
  private baseUrl = `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(SHOP_IP_URL) private apiUrl: string) { }

  getPurchase(): Observable<lkAlbom[]> {
    return this.http.get<lkAlbom[]>(`${this.baseUrl}lk/purchase`)
  }
}
