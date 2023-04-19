import { Component, OnInit } from '@angular/core';
import {SellerService} from "../../services/seller.service";
import {Router} from "@angular/router";
import {AlbomInfoSellerPage} from "../../models/AlbomInfoSellerPage";

@Component({
  selector: 'app-seller',
  templateUrl: './seller.component.html',
  styleUrls: ['./seller.component.css']
})
export class SellerComponent implements OnInit {
  titles: string[] = []
  currentTitle_update: string
  currentTitle_delete: string
  choosedAlbom_update: AlbomInfoSellerPage = {
    author: 'Выберите альбом',
    year: 2042,
    img_path: 'Выберите альбом',
    price: 2042,
    music_path: 'Выберите альбом'
  }
  choosedAlbom_delete: AlbomInfoSellerPage = {
    author: 'Выберите альбом',
    year: 2042,
    img_path: 'Выберите альбом',
    price: 2042,
    music_path: 'Выберите альбом'
  }
  constructor(private sellerService: SellerService, private router: Router) { }

  ngOnInit(): void {
    this.sellerService.getIndex().subscribe(res => {},
      err => this.router.navigate(['/home']))
    this.sellerService.getTitles().subscribe(res => this.titles = res)
    //this.sellerService.getInfo(this.titles[0]).subscribe(res => this.choosedAlbom = res)
    console.log(this.titles)
  }
  addAlbom(author: string,  title: string, year: number,
  img_path: string, price: number, music_path: string) {
    this.sellerService.addAlbom(author, title,year,img_path,price,music_path).subscribe(
      res => alert('ADDED'),
      err => alert('ERROR')
    )
    this.sellerService.getTitles().subscribe(res => this.titles = res)
  }
  UpdateInfoUpdate(title: string) {
    this.sellerService.getInfo(title).subscribe(res => this.choosedAlbom_update = res)
  }
  UpdateInfoDelete(title: string) {
    this.sellerService.getInfo(title).subscribe(res => this.choosedAlbom_delete = res)
  }
  updateAlbom(author: string,title:string, year: number,
              img_path: string, price: number, music_path: string) {
    return this.sellerService.updateAlbom(author, title, year,img_path,price,music_path).subscribe(
      res => alert('UPDATED'),
      err => alert('ERROR')
    )
    this.sellerService.getTitles().subscribe(res => this.titles = res)
  }
  deleteAlbom(title: string) {
    this.sellerService.deleteAlbom(title).subscribe(
      res => alert('DELETED'),
      err => alert('ERROR'))
    this.sellerService.getTitles().subscribe(res => this.titles = res)
  }
  click() {
    console.log(this.titles)
  }

}
