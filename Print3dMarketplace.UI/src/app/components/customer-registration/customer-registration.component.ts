import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { CustomerRegistrationRequestModel } from '../../models/user/customerRegistrationRequestModel';

@Component({
  selector: 'app-user-register',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string = '';

  get f() { return this.registerForm.controls; }

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService
  ) {}  

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(50)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
    },
    {
      validator: this.mustMatch('password', 'confirmPassword')
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    const customerRegistrationModel: CustomerRegistrationRequestModel = {
      email: this.f.email.value,
      password: this.f.password.value
    };

    this.userService.registerCustomer(customerRegistrationModel)
      .pipe(first())
      .subscribe(
        () => {
          this.router.navigate(['/user/login']);
        },
        err => {
          this.error = err.status === 400 ? err.error.message : "Unknown error!";
          this.loading = false;
        });
  }

  mustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {

      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustmatch) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustmatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }
}
