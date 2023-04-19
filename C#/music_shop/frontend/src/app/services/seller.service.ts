import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SHOP_IP_URL} from "../app-injection-tokens";
import {Observable} from "rxjs";
import {Card} from "../models/card";
import {AlbomInfoSellerPage} from "../models/AlbomInfoSellerPage";

@Injectable({
  providedIn: 'root'
})
export class SellerService {
  private baseUrl = `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(SHOP_IP_URL) private apiUrl: string) { }
  getIndex() {
    return this.http.get(`${this.baseUrl}seller`)
  }
  getTitles(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}seller/titles`)
  }
  addAlbom(author: string,  title: string, year: number,
           img_path: string, price: number, music_path: string) {
    return this.http.post(`${this.baseUrl}seller/add`,
      {
            "author": author,
            "title": title,
            "year": year,
            "img_path": img_path,
            "price": price,
            "music_path": music_path
    })
  }
  getInfo(title: string): Observable<AlbomInfoSellerPage> {
    return this.http.get<AlbomInfoSellerPage>(`${this.baseUrl}seller/info/` + title)
  }
  updateAlbom(author: string, title: string,  year: number,
              img_path: string, price: number, music_path: string) {
    return this.http.put(`${this.baseUrl}seller/update`,
      {
        "author": author,
        "title": title,
        "year": year,
        "img_path": img_path,
        "price": price,
        "music_path": music_path
      })
  }
  deleteAlbom(title: string) {
    return this.http.delete(`${this.baseUrl}seller/delete/` + title)
  }
}
