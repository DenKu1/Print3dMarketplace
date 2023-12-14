import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { UserModel } from '../../models/user/userModel';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  loading = false;
  submitted = false;
  error: string = '';

  userName: FormControl;

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
        this.router.navigate(['creator/profile']);
      }
      else {
        this.router.navigate(['customer/print-requests']);
      }
    }
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/user/login']);
  }

  findUser()
  {
    this.submitted = true;
    this.error = '';

    if (this.userName.invalid) {
      return;
    }

    this.loading = true;
/*    this.userService.getUserByUserName(this.userName.value)
      .pipe(first())
      .subscribe(
        user => {
          this.router.navigate(['/users', user.id, 'albums']);
        },
        err => {
          this.error = "User was not found";
          this.loading = false;
        });*/
  }
}
