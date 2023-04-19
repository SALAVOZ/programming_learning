import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface TracksList {
  name: string,
  authorName: string,
  albumImage: string,
  description: string,
  authorId: number,
  albumTracks: AlbumTracks[]
}
interface AlbumTracks {
  id: number,
  name: string,
  authorName: string,
  urlToDrive: string
}

@Injectable({ providedIn: 'root' })

export class TracksListService {
  constructor( private http: HttpClient ) { }

  albumId: number

  getTracksList(): Observable<TracksList[]> {
    return this.http.get<TracksList[]>(`https://localhost:44331/tracks/tracksList/${this.albumId}`)
    /*.pipe(delay(1000))*/
  }
}

