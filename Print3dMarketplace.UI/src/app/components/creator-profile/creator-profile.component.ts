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
import { PrinterModel } from '../../../models/printer/printerModel';
import { PrinterService } from '../../services/printer.service';
import { TemplatePrinterModel } from '../../../models/printer/templatePrinterModel';
import { NozzleModel } from '../../../models/printer/nozzleModel';

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

  printers: PrinterModel[];
  templatePrinters: TemplatePrinterModel[];
  nozzles: NozzleModel[];

  upCreatorInfo: UpdateCreatorInfo;
  upMaterials: UpdateMaterials;
  upPrinters: UpdatePrinters;
  
  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private materialService: MaterialService,
    private printerService: PrinterService,
    private toastrService: ToastrService)
  {
    this.upCreatorInfo = new UpdateCreatorInfo(this.formBuilder);
    this.upMaterials = new UpdateMaterials(this.formBuilder);
    this.upPrinters = new UpdatePrinters(this.formBuilder);
  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == false) {
      this.router.navigate(['/users/login']);
    }

    this.getCreatorInfo();

    this.getTemplateMaterials();
    this.getColors();
    this.getMaterials();

    this.getTemplatePrinters();
    this.getNozzles();
    this.getPrinters();
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

  getPrinters(): void {
    this.printerService.getAllCreatorPrinters(this.currentUser.id).subscribe(
      printers => {
        this.printers = printers;
        this.upPrinters.initialize(printers);
      })
  }

  getTemplatePrinters(): void {
    this.printerService.getAllTemplatePrinters().subscribe(
      templatePrinters => {
        this.templatePrinters = templatePrinters;
      })
  }

  getNozzles(): void {
    this.printerService.getAllNozzles().subscribe(
      nozzles => {
        this.nozzles = nozzles;
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
        isActive: group.get('isActive').value
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

  updatePrinters(): void {
    this.upPrinters.submitted = true;

    if (this.upPrinters.form.invalid) {
      return;
    }

    this.upPrinters.loading = true;

    const printerModels: PrinterModel[] = this.upPrinters.printersFormGroupArray.map(group => {
      return {
        nozzleId: group.get('nozzleId').value,
        templatePrinterId: group.get('templatePrinterId').value,
        modelName: group.get('modelName').value,
        printAreaLength: group.get('printAreaLength').value,
        printAreaWidth: group.get('printAreaWidth').value,
        printAreaHeight: group.get('printAreaHeight').value,
        isActive: group.get('isActive').value
      };
    });

    this.printerService.updateCreatorPrinters(this.currentUser.id, printerModels)
      .pipe(first())
      .subscribe(isUpdated => {
        this.upPrinters.loading = false;

        if (isUpdated) {
          this.toastrService.success("Printers updated successfully");
          this.printers = printerModels;
          this.upPrinters.disable();
        } else {
          this.toastrService.error("Unknown error! Please try again");
        }
      }, error => {
        this.upPrinters.loading = false;
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
      isActive: [{ value: materialModel.isActive, disabled: true }, [Validators.required]]
    }))
  }

  addEmptyMaterialModel() {
    const formArray = this.form.get('materials') as FormArray;

    formArray.push(this.formBuilder.group({
      colorId: [{ value: '', disabled: true }, [Validators.required]],
      templateMaterialId: [{ value: '', disabled: true }, [Validators.required]],
      name: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
      isActive: [{ value: false, disabled: true }, [Validators.required]]
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

class UpdatePrinters {
  loading = false;
  submitted = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {

    this.form = formBuilder.group({
      printers: formBuilder.array([])
    })

    this.form.disable();
  }

  initialize(printerModels: PrinterModel[]): void {

    printerModels.forEach(x => this.addPrinterModel(x));

    this.form.markAsUntouched();
  }

  enable(): void {
    this.form.enable();
  }

  disable(): void {
    this.form.disable();
  }

  addPrinterModel(printerModel: PrinterModel) {
    const formArray = this.form.get('printers') as FormArray;

    formArray.push(this.formBuilder.group({
      nozzleId: [{ value: printerModel.nozzleId, disabled: true }, [Validators.required]],
      templatePrinterId: [{ value: printerModel.templatePrinterId, disabled: true }, [Validators.required]],
      modelName: [{ value: printerModel.modelName, disabled: true }, [Validators.required, Validators.maxLength(50)]],
      printAreaLength: [{ value: printerModel.printAreaLength, disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaWidth: [{ value: printerModel.printAreaWidth, disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaHeight: [{ value: printerModel.printAreaHeight, disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      isActive: [{ value: printerModel.isActive, disabled: true }, [Validators.required]]
    }))
  }

  addEmptyPrinterModel() {
    const formArray = this.form.get('printers') as FormArray;

    formArray.push(this.formBuilder.group({
      nozzleId: [{ value: '', disabled: true }, [Validators.required]],
      templatePrinterId: [{ value: '', disabled: true }, [Validators.required]],
      modelName: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
      printAreaLength: [{ value: '', disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaWidth: [{ value: '', disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaHeight: [{ value: '', disabled: true }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      isActive: [{ value: false, disabled: true }, [Validators.required]]
    }))
  }

  deletePrinterModel(index: number) {
    const formArray = this.form.get('printers') as FormArray;
    formArray.removeAt(index)
  }

  get printersFormGroupArray(): FormGroup[] {
    return (this.form.get('printers') as FormArray).controls as FormGroup[];
  }
}
