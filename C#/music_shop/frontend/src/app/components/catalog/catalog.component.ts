import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms'
import {StoreService} from "../../services/store.service";
import {Albom} from "../../models/albom";


@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  form: FormGroup
  search = ''
  alboms: Albom[] = []


  constructor(private ss: StoreService) { }

  ngOnInit() {
    this.ss.getCatalog(this.search).subscribe(res => {this.alboms = res});
    this.form = new FormGroup({
      inp: new FormControl('')
    })
  }

  searchAlbom(search: string) {
    this.ss.getCatalog(this.search).subscribe(res => {this.alboms = res})
  }
  submit() {
    console.log('Form suki', this.form)
    this.search = this.form.value.inp
    console.log('search suki is', this.search)
  }
  addAtCart(id_albom: number) {
    console.log(this.alboms)
    this.ss.addOrder(id_albom).subscribe( () => alert('added') )
  }
}
