import { Component, OnInit } from '@angular/core';
import {LkService} from "../../../services/lk.service";
import {lkAlbom} from "../../../models/lkAlbom";

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.css']
})
export class PurchaseHistoryComponent implements OnInit {
  alboms: lkAlbom[] = []
  constructor(private lks: LkService) { }

  ngOnInit(): void {
    this.lks.getPurchase().subscribe(res => this.alboms = res, err => alert('ERROR'))
  }

}
