import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
export interface AlbumsList {
  id: number,
  name: string,
  fullAlbums: FullAlbums[]
}
interface FullAlbums {
  id: number,
  name: string,
  year: number,
  urlToImg: string
}

@Injectable({
  providedIn: 'root'
})
export class AlbumsListService {

  constructor(private http: HttpClient) { }

  authorId = 1

  getAlbumsList(): Observable<AlbumsList[]> {
    return this.http.get<AlbumsList[]>(`https://localhost:44331/authors/albumsList/${this.authorId}`)
    /*.pipe(delay(1000))*/
  }
}
