import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlbumsList, AlbumsListService } from 'app/services/albums-list.service';
import { AuthorInfo, AuthorPageService } from 'app/services/author-page.service';
import { TracksListService } from 'app/services/tracks-list.service';

@Component({
  selector: 'app-author-page',
  templateUrl: './author-page.component.html',
  styleUrls: ['./author-page.component.css']
})
export class AuthorPageComponent implements OnInit {

  constructor(
    private authorPageService: AuthorPageService,
    private albumsListService: AlbumsListService,
    private tracksListService: TracksListService,
    private router: Router) { }

  authorInfo: AuthorInfo[] = []
  albumsList: AlbumsList[] = []

  ngOnInit(): void {
    this.authorPageService.getAuthorInfo()
      .subscribe(authorInfo=>{
        console.log('Response', authorInfo)
        this.authorInfo = authorInfo
      })
  }

  toggleAlbums: boolean;
  toggleSingles: boolean;
  toggleInfo: boolean = true;

  goToAlbumPage(albumId: number) {
    this.tracksListService.albumId = albumId
    this.router.navigate(['/album-page'])
  }

  getAlbums(id: number) {
    this.albumsListService.getAlbumsList()
      .subscribe(albumsList=>{
        /*console.log('Response', albumsList)*/
        this.albumsList = albumsList
      })
    this.toggleAlbums = !this.toggleAlbums;
    this.toggleInfo = !this.toggleInfo;
  }
  getSingles() {
    this.toggleSingles = !this.toggleSingles;
    this.toggleInfo = !this.toggleInfo;
  }

}
