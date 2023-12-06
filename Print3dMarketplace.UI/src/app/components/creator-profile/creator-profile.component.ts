import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../models/user/userModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreatorModel } from '../../../models/user/creatorModel';
import { first } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

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
    private userService: UserService,
    private toastrService: ToastrService)
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
    this.upCreatorInfo.submitted = true;

    if (this.upCreatorInfo.form.invalid) {
      return;
    }

    this.upCreatorInfo.loading = true;

    var creatorModel: CreatorModel =
    {
      phoneNumber: this.upCreatorInfo.f.phoneNumber.value,
      alternativePhoneNumber: this.upCreatorInfo.f.alternativePhoneNumber.value,
      address: this.upCreatorInfo.f.address.value,
      description: this.upCreatorInfo.f.description.value,
    }

    this.userService.updateCreator(this.currentUser.id, creatorModel)
      .pipe(first())
      .subscribe(
        isUpdated => {
          if (isUpdated) {
            this.toastrService.success("Updated successfully");

            this.upCreatorInfo.form.markAsUntouched();

            this.creatorInfo = creatorModel;
          }

          this.upCreatorInfo.loading = false;
          this.upCreatorInfo.disable();
        },
        err => {
          this.toastrService.error("Unknown error! Please try again");

          this.upCreatorInfo.loading = false;
          this.upCreatorInfo.disable();
        });
  }

}
class UpdateCreatorInfo {
  loading = false;
  submitted = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      phoneNumber: [{ value: '', disabled: true }, [Validators.required, Validators.pattern("\\d{12}")]],
      alternativePhoneNumber: [{ value: '', disabled: true }, [Validators.required, Validators.pattern("\\d{12}")]],
      address: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
      description: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
    });

    this.form.disable();
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

  enable(): void {
    this.form.enable();
  }

  disable(): void {
    this.form.disable();
  }
}
