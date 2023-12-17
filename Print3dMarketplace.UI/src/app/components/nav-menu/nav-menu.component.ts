import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { FormControl } from '@angular/forms';
import { UserModel } from '../../models/user/userModel';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  currentUser: UserModel;

  constructor(
    private router: Router,
    private userService: UserService
  ) {
  }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }

  profile() {
    if (this.currentUser) {

      if (this.currentUser.isCreator) {
        this.router.navigate(['creator', this.currentUser.id, 'profile']);
      }
      else {
        this.router.navigate(['customer', this.currentUser.id, 'print-requests']);
      }
    }
  }

  createPrintRequest() {
    if (this.currentUser) {

      if (!this.currentUser.isCreator) {
        this.router.navigate(['customer', this.currentUser.id, 'print-request-creation']);
      }
    }
  }

  printRequests() {
    if (this.currentUser) {

      if (this.currentUser.isCreator) {
        this.router.navigate(['creator', this.currentUser.id, 'print-requests']);
      }
      else {
        this.router.navigate(['customer', this.currentUser.id, 'print-requests']);
      }
    }
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/user/login']);
  }
}
