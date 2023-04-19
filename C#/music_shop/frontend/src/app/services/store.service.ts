import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SHOP_IP_URL} from "../app-injection-tokens";
import {Observable} from "rxjs";
import {Card} from "../models/card";

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private baseUrl = `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(SHOP_IP_URL) private apiUrl: string) { }

  getOrders(): Observable<Card[]> {
    return this.http.get<Card[]>(`${this.baseUrl}orders`)
  }

  getCatalog(search: string): Observable<Card[]> {
    return this.http.get<Card[]>(`${this.baseUrl}alboms/` + search)
  }

  deleteOrder(id: number) {
    return this.http.delete(`${this.baseUrl}orders/delete/`+ id);
  }
  addOrder(albom_id: number) {
    return this.http.post<void>(`${this.baseUrl}alboms/add`, { "id": albom_id }  )
  }
  buy() {
    return this.http.get(`${this.baseUrl}orders/buy`)
  }
}
