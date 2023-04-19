import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Card} from "../../../models/card";

export interface deleteObj {
  albom_id: number,
  priceAll: number
}

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})
export class ShowComponent implements OnInit {

  count: number = 1
  priceAll: number = 0
  deleteObject: deleteObj
  @Input() card: Card
  @Output() onRemove = new EventEmitter<deleteObj>()
  @Output() onPlusTotal = new EventEmitter<number>()
  @Output() onMinusTotal = new EventEmitter<number>()
  @Output() onTotal = new EventEmitter<number>()

  deleteAlbom() {
    this.deleteObject = {albom_id: this.card.albom_id, priceAll: this.priceAll}
    this.onRemove.emit(this.deleteObject)
  }
  constructor() { }


  ngOnInit(): void {
    this.priceAll = this.card.price * this.count
    this.onTotal.emit(this.card.price)
  }
  increase() {
    this.count++
    this.onPlusTotal.emit(this.card.price)
    this.priceAll = this.card.price * this.count
  }
  decrease(): number {
    if( (this.count - 1) < 0) return 0
    else
    {
      this.count = this.count - 1
      this.onMinusTotal.emit(this.card.price)
      this.priceAll = this.card.price * this.count
      return 0
    }
  }

 // ngOnDestroy(): void {
//
  //}
}


