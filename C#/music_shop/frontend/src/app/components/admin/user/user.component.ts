import {Component, Input, OnInit} from '@angular/core';
import {UserModelAdminPage} from "../../../models/userModelAdminPage";
import {AdminService} from "../../../services/admin.service";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @Input() user: UserModelAdminPage
  roles: string[] = []
  newRole: string = ''
  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.getRoles().subscribe(res => this.roles = res)
    this.newRole = this.user.role
  }
  changeRole(userId: string ,role: string) {
    console.log(this.newRole)
    return this.adminService.changeRole(userId, role).subscribe(
      res => alert('CHANGED'),
      err => alert('ERROR')
    )
  }

}
