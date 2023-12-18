import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MaterialService } from '../../services/material.service';
import { ToastrService } from 'ngx-toastr';
import { TemplateMaterialModel } from '../../models/material/templateMaterialModel';
import { ColorModel } from '../../models/material/colorModel';
import { first, forkJoin } from 'rxjs';
import { UserModel } from '../../models/user/userModel';
import { PrintRequestService } from '../../services/print-request.service';
import { CreatePrintRequestModel } from '../../models/print-requests/createPrintRequestModel';
import { AbstractControl, ValidatorFn } from '@angular/forms';


export function ValidateFileExtension(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {

    var fileExtension = control.value?.split('.').slice(-1)[0];

    if (fileExtension === undefined) {
      return null;
    }

    if (fileExtension === "stl") {
      return null;
    }

    return { validateExtension: true };
  }
}

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

  onFileChange(event: any) {
    this.crPrintRequestFormInfo.f.fileName?.setValue(event.target.files[0]);
  }

  initPrintRequestForm(): void {
    forkJoin({
      colors: this.materialService.getAllColors(),
      templateMaterials: this.materialService.getAllTemplateMaterials()
    }).subscribe(({ colors, templateMaterials }) => {
      this.crPrintRequestFormInfo.initialize(templateMaterials, colors);
    });
  }

  createPrintRequest(): void {
    this.crPrintRequestFormInfo.submitted = true;

    if (this.crPrintRequestFormInfo.form.invalid) {
      return;
    }
    
    const reader = new FileReader();
    reader.onload = (event) => {
      const filecontent = (event.target as FileReader).result as string

      var printRequest: CreatePrintRequestModel =
      {
        templateMaterialId: this.crPrintRequestFormInfo.f.templateMaterialId.value,
        colorId: this.crPrintRequestFormInfo.f.colorId.value,

        layerHeight: this.crPrintRequestFormInfo.f.layerHeight.value,
        infill: this.crPrintRequestFormInfo.f.infill.value,

        printAreaLength: this.crPrintRequestFormInfo.f.printAreaLength.value,
        printAreaWidth: this.crPrintRequestFormInfo.f.printAreaWidth.value,
        printAreaHeight: this.crPrintRequestFormInfo.f.printAreaHeight.value,

        fileName: this.crPrintRequestFormInfo.f.formFile.value.name,
        fileContent: filecontent,

        comment: this.crPrintRequestFormInfo.f.comment.value,
        useSupports: this.crPrintRequestFormInfo.f.useSupports.value,
        wallThickness: this.crPrintRequestFormInfo.f.wallThickness.value
      }

      this.printRequestService.createPrintRequest(printRequest)
        .pipe(first())
        .subscribe(
          isUpdated => {
            this.crPrintRequestFormInfo.loading = false;

            if (isUpdated) {
              this.toastrService.success("Print request created successfully");

              this.crPrintRequestFormInfo.form.markAsUntouched();

              this.router.navigate(['customer', this.currentUser.id, 'print-requests']);
            } else {
              this.toastrService.error("Unknown error! Please try again");
            }
          },
          err => {
            this.crPrintRequestFormInfo.loading = false;
            this.toastrService.error("Unknown error! Please try again");
          });
    };

    reader.readAsDataURL(this.crPrintRequestFormInfo.f.formFile.value);
  }
}
class CreatePrintRequestFormInfo {
  loading = false;
  submitted = false;

  form: FormGroup;
  templateMaterials: TemplateMaterialModel[];
  colors: ColorModel[];
  allowedExtensions: string[];

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

      formFile: ['', [Validators.required, ValidateFileExtension()]],

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
