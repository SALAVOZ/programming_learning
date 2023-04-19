import { Component, OnInit, Input } from '@angular/core';
import {deleteObj, ShowComponent} from "./show/show.component";
import {AuthService} from "../../services/auth.service";
import {Card} from "../../models/card";
import {StoreService} from "../../services/store.service";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {

  total: number = 0
  cards: Card[] = []
  constructor(private as: AuthService, private ss: StoreService) { }

  ngOnInit(): void {
    this.ss.getOrders().subscribe(res => {this.cards = res} );
    console.log(this.cards);
    //for (let i = 0; i < this.cards.length; i++)
    //  this.total = this.total + this.cards[i].price
  }
  removeAlbom(obj: deleteObj) {

    this.ss.deleteOrder(obj.albom_id).subscribe( () => alert('deleted') )
    this.cards = this.cards.filter(card => card.albom_id !== obj.albom_id)
    this.total = this.total - obj.priceAll
  }
  makePlusTotal(price: number) {
    this.total = this.total + price
  }
  makeMinusTotal(price: number) {
    this.total = this.total - price
  }
  initialTotal(price: number) {
    this.total = this.total + price
  }
  buy() {
    return this.ss.buy().subscribe(res => alert('BOUGHT'), err => alert('ERROR'))
  }
}
