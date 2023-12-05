import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../models/user/userModel';
import { FormBuilder } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreatorModel } from '../../../models/user/creatorModel';

@Component({
  selector: 'creator-profile',
  templateUrl: './creator-profile.component.html',
  styleUrls: ['./creator-profile.component.css']
})
export class CreatorProfileComponent implements OnInit {
  currentUser: UserModel;
  creatorInfo: CreatorModel;
  
  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private userService: UserService)
  {
  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == false) {
      this.router.navigate(['/users/login']);
    }

    this.getCreatorInfo();
  }

  getCreatorInfo(): void {
    this.userService.getCreatorById(this.currentUser.id).subscribe(
      creator => { this.creatorInfo = creator; })
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }
}
