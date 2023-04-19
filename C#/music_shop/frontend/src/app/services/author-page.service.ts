import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface AuthorInfo {
  id: number,
  name: string,
  information: string,
  urlToImg: string
}
@Injectable({
  providedIn: 'root'
})
export class AuthorPageService {

  constructor( private http: HttpClient ) { }

  authorId: number

  getAuthorInfo(): Observable<AuthorInfo[]> {
    return this.http.get<AuthorInfo[]>(`https://localhost:44331/albums/authorInfo/${this.authorId}`)
    /*.pipe(delay(1000))*/
  }
}

