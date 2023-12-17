import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../models/user/userModel';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreatorModel } from '../../models/user/creatorModel';
import { first, forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MaterialModel } from '../../models/material/materialModel';
import { MaterialService } from '../../services/material.service';
import { TemplateMaterialModel } from '../../models/material/templateMaterialModel';
import { ColorModel } from '../../models/material/colorModel';
import { PrinterModel } from '../../models/printer/printerModel';
import { PrinterService } from '../../services/printer.service';
import { TemplatePrinterModel } from '../../models/printer/templatePrinterModel';
import { NozzleModel } from '../../models/printer/nozzleModel';

@Component({
  selector: 'creator-profile',
  templateUrl: './creator-profile.component.html',
  styleUrls: ['./creator-profile.component.css']
})
export class CreatorProfileComponent implements OnInit {
  isOwned: boolean;
  ownerId: string;

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

    this.initCreatorInfo();
    this.initMaterials();
    this.initPrinters();
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
    this.activeRoute.params.subscribe(routeParams => {
      this.isOwned = this.currentUser.id == routeParams.id;
      this.ownerId = routeParams.id;
    });
  }

  initCreatorInfo(): void {
    this.userService.getCreator(this.ownerId).subscribe(
      creator =>
      {
        this.creatorInfo = creator;
        this.upCreatorInfo.initialize(creator);
      })
  }

  initMaterials(): void {
    forkJoin({
      colors: this.materialService.getAllColors(),
      templateMaterials: this.materialService.getAllTemplateMaterials(),
      materials: this.materialService.getAllCreatorMaterials(this.ownerId)
    })
      .subscribe(({ colors, templateMaterials, materials }) => {
        this.colors = colors;
        this.templateMaterials = templateMaterials;
        this.materials = materials;

        this.upMaterials.initialize(materials);
      });
  }

  initPrinters(): void {
    forkJoin({
      nozzles: this.printerService.getAllNozzles(),
      templatePrinters: this.printerService.getAllTemplatePrinters(),
      printers: this.printerService.getAllCreatorPrinters(this.ownerId)
    })
      .subscribe(({ nozzles, templatePrinters, printers }) => {
        this.nozzles = nozzles;
        this.templatePrinters = templatePrinters;
        this.printers = printers;

        this.upPrinters.initialize(printers);
      });
  }

  updateCreatorInfo(): void {
    if (!this.isOwned || this.upCreatorInfo.form.invalid) {
      return;
    }

    this.upCreatorInfo.loading = true;

    var creatorModel: CreatorModel =
    {
      companyName: this.upCreatorInfo.f.companyName.value,
      phoneNumber: this.upCreatorInfo.f.phoneNumber.value,
      alternativePhoneNumber: this.upCreatorInfo.f.alternativePhoneNumber.value,
      address: this.upCreatorInfo.f.address.value,
      description: this.upCreatorInfo.f.description.value,
    }

    this.userService.updateCreator(this.currentUser.id, creatorModel)
      .pipe(first())
      .subscribe(
        isUpdated => {
          this.upCreatorInfo.save();

          if (isUpdated) {
            this.toastrService.success("Updated successfully");
            this.creatorInfo = creatorModel;
          } else {
            this.toastrService.error("Unknown error! Please try again");
          }

          this.upCreatorInfo.loading = false;
        },
        err => {
          this.upCreatorInfo.save();
          this.toastrService.error("Unknown error! Please try again");
          this.upCreatorInfo.loading = false;
        });
  }

  updateMaterials(): void {
    if (!this.isOwned ||this.upMaterials.form.invalid) {
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
        this.upMaterials.save();

        if (isUpdated) {
          this.toastrService.success("Materials updated successfully");
          this.materials = materialModels;
        } else {
          this.toastrService.error("Unknown error! Please try again");
        }

        this.upMaterials.loading = false;
      }, error => {
        this.upMaterials.save();
        this.toastrService.error("Unknown error! Please try again");
        this.upMaterials.loading = false;
      });
  }

  updatePrinters(): void {
    if (!this.isOwned ||this.upPrinters.form.invalid) {
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
        this.upPrinters.save();

        if (isUpdated) {
          this.toastrService.success("Printers updated successfully");
          this.printers = printerModels;
        } else {
          this.toastrService.error("Unknown error! Please try again");
        }

        this.upPrinters.loading = false;
      }, error => {
        this.upPrinters.save();
        this.toastrService.error("An error occurred");
        this.upPrinters.loading = false;
      });
  }

  onTemplateMaterialSelected(index: number, event: any): void {
    if (event.target.value) {
      const selectedTemplateMaterial = this.templateMaterials.find(t => t.id === event.target.value);
      if (selectedTemplateMaterial) {
        const formArray = this.upMaterials.form.get('materials') as FormArray;
        const formGroup = formArray.at(index) as FormGroup;
        formGroup.controls['name'].setValue(selectedTemplateMaterial.name);
      }
    }
  }

  onTemplatePrinterSelected(index: number, event: any): void {
    if (event.target.value) {
      const selectedTemplatePrinter = this.templatePrinters.find(t => t.id === event.target.value);
      if (selectedTemplatePrinter) {
        const formArray = this.upPrinters.form.get('printers') as FormArray;
        const formGroup = formArray.at(index) as FormGroup;

        formGroup.controls['modelName'].setValue(selectedTemplatePrinter.modelName);
        formGroup.controls['printAreaLength'].setValue(selectedTemplatePrinter.printAreaLength);
        formGroup.controls['printAreaWidth'].setValue(selectedTemplatePrinter.printAreaWidth);
        formGroup.controls['printAreaHeight'].setValue(selectedTemplatePrinter.printAreaHeight);
      }
    }
  }
}

class UpdateCreatorInfo {
  loading: boolean = false;
  canBeEdited: boolean = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      companyName: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(15)]],
      phoneNumber: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.pattern("\\d{12}")]],
      alternativePhoneNumber: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.pattern("\\d{12}")]],
      address: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
      description: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
    });
  }

  initialize(creatorInfo: CreatorModel): void {
    this.form.setValue({
      companyName: creatorInfo.companyName,
      phoneNumber: creatorInfo.phoneNumber,
      alternativePhoneNumber: creatorInfo.alternativePhoneNumber,
      address: creatorInfo.address,
      description: creatorInfo.description
    });
    this.form.markAsUntouched();
  }

  edit(): void {
    this.canBeEdited = true;
    this.form.enable();
  }

  save(): void {
    this.canBeEdited = false;
    this.form.disable();
    this.form.markAsUntouched();
  }
}

class UpdateMaterials {
  loading: boolean = false;
  canBeEdited: boolean = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {

    this.form = formBuilder.group({
      materials: formBuilder.array([])
    });
  }

  initialize(materialModels: MaterialModel[]): void {

    materialModels.forEach(x => this.addMaterialModel(x));

    this.form.markAsUntouched();
  }

  addMaterialModel(materialModel: MaterialModel) {
    const formArray = this.form.get('materials') as FormArray;

    formArray.push(this.formBuilder.group({
      colorId: [{ value: materialModel.colorId, disabled: !this.canBeEdited }, [Validators.required]],
      templateMaterialId: [{ value: materialModel.templateMaterialId, disabled: !this.canBeEdited }, [Validators.required]],
      name: [{ value: materialModel.name, disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
      isActive: [{ value: materialModel.isActive, disabled: !this.canBeEdited }, [Validators.required]]
    }));
  }

  addEmptyMaterialModel() {
    const formArray = this.form.get('materials') as FormArray;

    formArray.push(this.formBuilder.group({
      colorId: [{ value: '', disabled: !this.canBeEdited }, [Validators.required]],
      templateMaterialId: [{ value: '', disabled: !this.canBeEdited }, [Validators.required]],
      name: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
      isActive: [{ value: false, disabled: !this.canBeEdited }, [Validators.required]]
    }))
  }

  deleteMaterialModel(index: number) {
    const formArray = this.form.get('materials') as FormArray;
    formArray.removeAt(index);
  }

  get materialsFormGroupArray(): FormGroup[] {
    return (this.form.get('materials') as FormArray).controls as FormGroup[];
  }

  edit(): void {
    this.canBeEdited = true;
    this.form.enable();
  }

  save(): void {
    this.canBeEdited = false;
    this.form.disable();
    this.form.markAsUntouched();
  }
}

class UpdatePrinters {
  loading: boolean = false;
  canBeEdited: boolean = false;

  form: FormGroup;

  get f() { return this.form.controls; }

  constructor(private formBuilder: FormBuilder) {

    this.form = formBuilder.group({
      printers: formBuilder.array([])
    });
  }

  initialize(printerModels: PrinterModel[]): void {

    printerModels.forEach(x => this.addPrinterModel(x));

    this.form.markAsUntouched();
  }

  addPrinterModel(printerModel: PrinterModel) {
    const formArray = this.form.get('printers') as FormArray;

    formArray.push(this.formBuilder.group({
      nozzleId: [{ value: printerModel.nozzleId, disabled: !this.canBeEdited }, [Validators.required]],
      templatePrinterId: [{ value: printerModel.templatePrinterId, disabled: !this.canBeEdited }, [Validators.required]],
      modelName: [{ value: printerModel.modelName, disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
      printAreaLength: [{ value: printerModel.printAreaLength, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaWidth: [{ value: printerModel.printAreaWidth, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaHeight: [{ value: printerModel.printAreaHeight, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      isActive: [{ value: printerModel.isActive, disabled: !this.canBeEdited }, [Validators.required]]
    }));
  }

  addEmptyPrinterModel() {
    const formArray = this.form.get('printers') as FormArray;

    formArray.push(this.formBuilder.group({
      nozzleId: [{ value: '', disabled: !this.canBeEdited }, [Validators.required]],
      templatePrinterId: [{ value: '', disabled: !this.canBeEdited }, [Validators.required]],
      modelName: [{ value: '', disabled: !this.canBeEdited }, [Validators.required, Validators.maxLength(50)]],
      printAreaLength: [{ value: 0, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaWidth: [{ value: 0, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      printAreaHeight: [{ value: 0, disabled: !this.canBeEdited }, [Validators.required, Validators.min(0), Validators.max(1000)]],
      isActive: [{ value: false, disabled: !this.canBeEdited }, [Validators.required]]
    }));
  }

  deletePrinterModel(index: number) {
    const formArray = this.form.get('printers') as FormArray;
    formArray.removeAt(index);
  }

  get printersFormGroupArray(): FormGroup[] {
    return (this.form.get('printers') as FormArray).controls as FormGroup[];
  }

  edit(): void {
    this.canBeEdited = true;
    this.form.enable();
  }

  save(): void {
    this.canBeEdited = false;
    this.form.disable();
    this.form.markAsUntouched();
  }
}
