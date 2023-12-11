import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../models/user/userModel';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreatorModel } from '../../../models/user/creatorModel';
import { first } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MaterialModel } from '../../../models/material/materialModel';
import { MaterialService } from '../../services/material.service';
import { TemplateMaterialModel } from '../../../models/material/templateMaterialModel';
import { ColorModel } from '../../../models/material/colorModel';

@Component({
  selector: 'creator-profile',
  templateUrl: './creator-profile.component.html',
  styleUrls: ['./creator-profile.component.css']
})
export class CreatorProfileComponent implements OnInit {
  currentUser: UserModel;
  creatorInfo: CreatorModel;

  materials: MaterialModel[];
  templateMaterials: TemplateMaterialModel[];
  colors: ColorModel[];

  upCreatorInfo: UpdateCreatorInfo;
  upMaterials: UpdateMaterials;
  
  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private materialService: MaterialService,
    private toastrService: ToastrService)
  {
    this.upCreatorInfo = new UpdateCreatorInfo(this.formBuilder);
    this.upMaterials = new UpdateMaterials(this.formBuilder);


  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == false) {
      this.router.navigate(['/users/login']);
    }

    this.getCreatorInfo();
    this.getMaterials();
    this.getTemplateMaterials();
    this.getColors();
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

  getMaterials(): void {
    this.materialService.getAllCreatorMaterials(this.currentUser.id).subscribe(
      materials => {
        this.materials = materials;
        this.upMaterials.initialize(materials);
      })
  }

  getTemplateMaterials(): void {
    this.materialService.getAllTemplateMaterials().subscribe(
      templateMaterials => {
        this.templateMaterials = templateMaterials;
      })
  }

  getColors(): void {
    this.materialService.getAllColors().subscribe(
      colors => {
        this.colors = colors;
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

  updateMaterials(): void {
    this.upMaterials.submitted = true;

    if (this.upMaterials.form.invalid) {
      return;
    }

    this.upMaterials.loading = true;

    const materialModels: MaterialModel[] = this.upMaterials.materialsFormGroupArray.map(group => {
      return {
        colorId: group.get('colorId').value,
        templateMaterialId: group.get('templateMaterialId').value,
        name: group.get('name').value,
        isActive: group.get('isActive').value,
        // Add other properties as necessary
      };
    });

    this.materialService.updateCreatorMaterials(this.currentUser.id, materialModels)
      .pipe(first())
      .subscribe(isUpdated => {
        this.upMaterials.loading = false;

        if (isUpdated) {
          this.toastrService.success("Materials updated successfully");
          this.materials = materialModels;
          this.upMaterials.disable();
        } else {
          this.toastrService.error("Unknown error! Please try again");
        }
      }, error => {
        this.upMaterials.loading = false;
        this.toastrService.error("An error occurred");
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

class UpdateMaterials {
  loading = false;
  submitted = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {

    this.form = formBuilder.group({
      materials: formBuilder.array([])
    })

    this.form.disable();
  }

  initialize(materialModels: MaterialModel[]): void {

    materialModels.forEach(x => this.addMaterialModel(x));

    this.form.markAsUntouched();
  }

  enable(): void {
    this.form.enable();
  }

  disable(): void {
    this.form.disable();
  }

  addMaterialModel(materialModel: MaterialModel) {
    const formArray = this.form.get('materials') as FormArray;

    formArray.push(this.formBuilder.group({
      colorId: [{ value: materialModel.colorId, disabled: true }, [Validators.required]],
      templateMaterialId: [{ value: materialModel.templateMaterialId, disabled: true }, [Validators.required]],
      name: [{ value: materialModel.name, disabled: true }, [Validators.required, Validators.maxLength(50)]],
      isActive: [{ value: materialModel.isActive, disabled: true }, [Validators.required, Validators.pattern("\\d{12}")]]
    }))
  }

  addEmptyMaterialModel() {
    const formArray = this.form.get('materials') as FormArray;

    formArray.push(this.formBuilder.group({
      colorId: [{ value: '', disabled: true }, [Validators.required]],
      templateMaterialId: [{ value: '', disabled: true }, [Validators.required]],
      name: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
      isActive: [{ value: false, disabled: true }, [Validators.required, Validators.pattern("\\d{12}")]]
    }))
  }

  deleteMaterialModel(index: number) {
    const formArray = this.form.get('materials') as FormArray;
    formArray.removeAt(index)
  }

  get materialsFormGroupArray(): FormGroup[] {
    return (this.form.get('materials') as FormArray).controls as FormGroup[];
  }
}
