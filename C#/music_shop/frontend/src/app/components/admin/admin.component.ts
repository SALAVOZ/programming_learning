import { Component, OnInit } from '@angular/core';
import {AdminService} from "../../services/admin.service";
import {Router} from "@angular/router";
import {UserModelAdminPage} from "../../models/userModelAdminPage";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  users: UserModelAdminPage[] = []
  constructor(private adminservice: AdminService, private router: Router) { }

  ngOnInit(): void {
    this.adminservice.getIndex().subscribe(
      res => {},
    err => {this.router.navigate(['/home'])})
    this.adminservice.getUsers().subscribe(res => this.users = res )
  }
  click() {
    console.log(this.users)
  }


}
