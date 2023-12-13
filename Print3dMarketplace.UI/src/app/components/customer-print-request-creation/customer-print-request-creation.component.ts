import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MaterialService } from '../../services/material.service';
import { ToastrService } from 'ngx-toastr';
import { TemplateMaterialModel } from '../../models/material/templateMaterialModel';
import { ColorModel } from '../../models/material/colorModel';
import { forkJoin } from 'rxjs';
import { UserModel } from '../../models/user/userModel';
import { PrintRequestService } from '../../services/print-request.service';

@Component({
  selector: 'customer-print-request-creation',
  templateUrl: './customer-print-request-creation.component.html',
  styleUrls: ['./customer-print-request-creation.component.css']
})
export class CustomerPrintRequestCreationComponent implements OnInit {
  currentUser: UserModel;
  crPrintRequestFormInfo: CreatePrintRequestFormInfo;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private materialService: MaterialService,
    private printRequestService: PrintRequestService,
    private toastrService: ToastrService) {
    this.crPrintRequestFormInfo = new CreatePrintRequestFormInfo(this.formBuilder);
  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == true) {
      this.userService.logout();
      this.router.navigate(['/user/login']);
    }

    this.initPrintRequestForm();
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }

  initPrintRequestForm(): void {
    forkJoin({
      colors: this.materialService.getAllColors(),
      templateMaterials: this.materialService.getAllTemplateMaterials()
    }).subscribe(({ colors, templateMaterials }) => {
      this.crPrintRequestFormInfo.initialize(colors, templateMaterials);
    });
  }

  createPrintRequest(): void {
    //this.printRequestService.createPrintRequest();
  }
}

class CreatePrintRequestFormInfo {
  loading = false;
  submitted = false;

  form: FormGroup;

  templateMaterials: TemplateMaterialModel[];
  colors: ColorModel[];

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      templateMaterialId: ['', [Validators.required]],
      colorId: ['', [Validators.required]],

      layerHeight: [0.2, [Validators.required, Validators.min(0), Validators.max(1)]],
      infill: [100, [Validators.required, Validators.min(0), Validators.max(100)]],

      printAreaLength: [0, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaWidth: [0, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaHeight: [0, [Validators.required, Validators.min(0), Validators.max(1000)]],

      comment: [null, [Validators.maxLength(200)]],
      useSupports: [null, []],
      wallThickness: [null, [Validators.min(0), Validators.max(1)]],
    });

    this.form.disable();
  }

  initialize(
    templateMaterials: TemplateMaterialModel[],
    colors: ColorModel[]): void {

    this.colors = colors;
    this.templateMaterials = templateMaterials;

    this.form.markAsUntouched();
    this.form.enable();
  }
}
