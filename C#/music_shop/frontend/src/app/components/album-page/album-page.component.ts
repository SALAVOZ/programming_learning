import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TracksList, TracksListService } from 'app/services/tracks-list.service';
import { AuthorPageService } from 'app/services/author-page.service';

@Component({
  selector: 'app-album-page',
  templateUrl: './album-page.component.html',
  styleUrls: ['./album-page.component.css']
})
export class AlbumPageComponent implements OnInit {

  constructor(
    private tracksListService: TracksListService,
    private authorPageService: AuthorPageService,
    private router: Router) {}

  tracksList: TracksList[] = []

  goToAuthorPage(authorId: number) {
    this.authorPageService.authorId = authorId
    this.router.navigate(['/author-page'])
  }

  ngOnInit(): void {
    this.tracksListService.getTracksList()
      .subscribe(tracksList=>{
        console.log('Response', tracksList)
        this.tracksList = tracksList
      })
  }

  audioSrc: string = '';
  togglePhotos: boolean;
  toggleInfo: boolean;

  getPhotos() {
    this.togglePhotos = !this.togglePhotos;
  }
  getInfo() {
    this.toggleInfo = !this.toggleInfo;
  }

  changeAudio(src: string) {
    this.audioSrc = src;
  }

}
