import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../models/user/userModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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

  upCreatorInfo: UpdateCreatorInfo;
  
  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private userService: UserService)
  {
    this.upCreatorInfo = new UpdateCreatorInfo(this.formBuilder);
  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == false) {
      this.router.navigate(['/users/login']);
    }

    this.getCreatorInfo();
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }

  getCreatorInfo(): void {
    this.userService.getCreator(this.currentUser.id).subscribe(
      creator =>
      {
        this.creatorInfo = creator;
        this.upCreatorInfo.initialize(creator);
      })
  }

  updateCreatorInfo(): void {
/*    this.userService.updateCreator(this.currentUser.id, creator).subscribe(
      creator => { this.creatorInfo = creator; })*/
  }
}
class UpdateCreatorInfo {
  isDisabled = true;
  loading = false;
  submitted = false;
  error: string = '';
  success: string = '';

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      phoneNumber: ['', [Validators.required, Validators.pattern("\\d{12}")]],
      alternativePhoneNumber: ['', [Validators.required, Validators.pattern("\\d{12}")]],
      address: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  initialize(creatorInfo: CreatorModel): void {
    this.form.setValue({
      phoneNumber: creatorInfo.phoneNumber,
      alternativePhoneNumber: creatorInfo.alternativePhoneNumber,
      address: creatorInfo.address,
      description: creatorInfo.description
    });
    this.form.markAsUntouched();
  }

  toggle(): void {
    this.isDisabled = !this.isDisabled;
  }
}
