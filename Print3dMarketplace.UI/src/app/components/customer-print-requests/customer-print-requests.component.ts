import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MaterialService } from '../../services/material.service';
import { ToastrService } from 'ngx-toastr';
import { TemplateMaterialModel } from '../../models/material/templateMaterialModel';
import { ColorModel } from '../../models/material/colorModel';
import { first, forkJoin } from 'rxjs';
import { UserModel } from '../../models/user/userModel';
import { PrintRequestService } from '../../services/print-request.service';
import { PrintRequestModel } from '../../models/print-requests/printRequestModel';

@Component({
  selector: 'customer-print-requests',
  templateUrl: './customer-print-requests.component.html',
  styleUrls: ['./customer-print-requests.component.css']
})
export class CustomerPrintRequestsComponent {
  currentUser: UserModel;

  printRequests: PrintRequestModel[];
  templateMaterials: TemplateMaterialModel[];
  colors: ColorModel[];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private materialService: MaterialService,
    private printRequestService: PrintRequestService,
    private toastrService: ToastrService) {
  }

  ngOnInit() {
    this.getCurrentUser();

    if (this.currentUser.isCreator == true) {
      this.userService.logout();
      this.router.navigate(['/user/login']);
    }

    forkJoin({
      colors: this.materialService.getAllColors(),
      templateMaterials: this.materialService.getAllTemplateMaterials(),
      printRequests: this.printRequestService.getCustomerPrintRequests()
    })
      .subscribe(({ colors, templateMaterials, printRequests }) => {
        this.colors = colors;
        this.templateMaterials = templateMaterials;
        this.printRequests = printRequests;
      });
  }


  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }

  getColorName(id: string): string {
    return this.colors.find(color => color.id === id)?.name
  }

  getMaterialName(id: string): string {
    return this.templateMaterials.find(material => material.id === id)?.name
  }
}
