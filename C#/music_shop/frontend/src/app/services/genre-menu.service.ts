import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";
export interface GenreMenu {
  id: number,
  name: string,
  fullAuthors: FullAuthors[]
}
interface FullAuthors {
  id: number,
  name: string
}

@Injectable({
  providedIn: 'root'
})
export class GenreMenuService {
  constructor(private http: HttpClient) { }

  getGenreMenu(): Observable<GenreMenu[]> {
    return this.http.get<GenreMenu[]>(`https://localhost:44331/genres/genresMenu`)
  }

  menuChoosen$ = new Subject<number>();

}

