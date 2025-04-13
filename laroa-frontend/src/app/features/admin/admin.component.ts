import { Component, OnInit } from '@angular/core';
import { Admin } from "../../entities/admin";
import { AdminService } from "../../services/admin.service";
import { UserService } from "../../services/user.service";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']  // Corrected property name
})
export class AdminComponent implements OnInit {
  admins: Admin[] = [];

  constructor(private adminService: AdminService, public userService: UserService) { }

  ngOnInit(): void {
    this.getAllAdmins();
  }

  getAllAdmins() {
    this.adminService.getAllAdmins().subscribe((admins) => {
      this.admins = admins;
      console.log("executat", this.admins);
    });
  }
}
