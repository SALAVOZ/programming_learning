import {Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlbumsList, AlbumsListService } from 'app/services/albums-list.service';
import { GenreMenuService } from 'app/services/genre-menu.service';
import { TracksListService } from 'app/services/tracks-list.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {

  constructor(
    private albumsListService: AlbumsListService,
    private tracksListService: TracksListService,
    private genreMenuService: GenreMenuService,
    private router: Router) {}

  count: number;
  greeting: string;
  audioSrc: string = '';
  chosenMenuIndex: number

  goToAlbumPage(albumId: number) {
    this.tracksListService.albumId = albumId
    this.router.navigate(['/album-page'])
  }

  albumsList: AlbumsList[] = []

  menu(index: number) {
    this.albumsListService.authorId = index
    this.albumsListService.getAlbumsList()
      .subscribe(albumsList=>{
        console.log('Response', albumsList)
        this.albumsList = albumsList
      })
  }

  ngOnInit(): void {

    // the subscription starts below
    this.genreMenuService.menuChoosen$.subscribe(idx => {
      this.chosenMenuIndex = idx;
      this.menu(this.chosenMenuIndex); // local function call
      console.log('menu index chosen: ', this.chosenMenuIndex);
    });

    this.greeting = 'Здравствуйте!';
    this.count = 1;

    setTimeout(() => {
      this.greeting = 'Добро пожаловать на сайт команды ИРЭТ - сила!';
      this.count = 2;
    }, 3000);

    setTimeout(() => {
      this.greeting = 'Настройте фильтры поиска!';
      this.count = 3;
    }, 6000);

    setTimeout(() => {
      this.greeting = 'Включите проигрыатель или откройте один из альбомов!';
      this.count = 4;
    }, 9000);

    setTimeout(() => {
      this.greeting = 'Made by ИРЭТ - сила, 2021';
      this.count = 5;
    }, 12000);
  }

}
