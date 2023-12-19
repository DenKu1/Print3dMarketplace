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
import { SubmitPrintRequestModel } from '../../models/print-requests/submitPrintRequestModel';
import { FileResponseModel } from '../../models/print-requests/fileResponse';

@Component({
  selector: 'customer-print-requests',
  templateUrl: './customer-print-requests.component.html',
  styleUrls: ['./customer-print-requests.component.css']
})
export class CustomerPrintRequestsComponent {
  isLoading: boolean = false;
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

    this.refreshPrintRequests();
  }
  
  refreshPrintRequests(): void {
    this.isLoading = true;

    forkJoin({
      colors: this.materialService.getAllColors(),
      templateMaterials: this.materialService.getAllTemplateMaterials(),
      printRequests: this.printRequestService.getCustomerPrintRequests()
    })
      .subscribe(({ colors, templateMaterials, printRequests }) => {
        this.colors = colors;
        this.templateMaterials = templateMaterials;
        this.printRequests = printRequests;

        this.isLoading = false;
      });
  }

  getCurrentUser(): void {
    this.userService.currentUser.subscribe(user => this.currentUser = user);
  }

  getColorName(id: string): string {
    return this.colors.find(color => color.id === id)?.name
  }

  dataURItoBlob(dataURI) {
     const byteString = window.atob(dataURI); 
     const arrayBuffer = new ArrayBuffer(byteString.length); 
     const int8Array = new Uint8Array(arrayBuffer); 
     for (let i = 0; i < byteString.length; i++) 
     { int8Array[i] = byteString.charCodeAt(i); } 
     const blob = new Blob([int8Array], 
      { type: 'file/stl' }); 
      return blob; 
  }
  downloadSTLScheme(id: string): void {
    let printRequests: FileResponseModel;

     this.printRequestService.downloadStlScheme(id).subscribe(model => {
     
      const base64 = model.data;
      const imageName = model.fileName;
      const blob = this.dataURItoBlob(base64);
      const url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }

  modifyModelTitle(fileName: string): string{
    var fileExtension = fileName?.split('.').slice(-1)[0];

     if(fileName.length > 20)
      return fileName.substring(0,20) + "..." +  fileExtension;
    
      return  fileName;
  }

  getMaterialName(id: string): string {
    return this.templateMaterials.find(material => material.id === id)?.name
  }

  isCustomerAbleToCancel(printRequest: PrintRequestModel): boolean {
    let newOrSubStatus: boolean = printRequest.printRequestStatusName === 'New' || printRequest.printRequestStatusName === 'CreatorSubmission';

    return newOrSubStatus;
  }

  isAnyCreatorAlreadySubmitted(printRequest: PrintRequestModel): boolean {
    let submittedByCreator: boolean = printRequest.printRequestStatusName === 'CreatorSubmission';

    return submittedByCreator;
  }

  isCustomerSubmittedForAnyCreator(printRequest: PrintRequestModel): boolean {
    let submittedByCustomer: boolean = printRequest.printRequestStatusName === 'CustomerSubmission';

    return submittedByCustomer;
  }

  cancelPrintRequest(id: string): void {
    this.printRequestService.cancelPrintRequest(id)
      .subscribe(
        isUpdated => {
          if (isUpdated) {
            this.toastrService.success("Print request canceled successfully");
            this.refreshPrintRequests();
          } else {
            this.toastrService.error("Unknown error! Please try again");
          }
        },
        err => {
          this.toastrService.error("Unknown error! Please try again");
        });
  }

  sumbitCreator(printRequestId: string, creatorId: string) {

    let submitPrintRequestModel: SubmitPrintRequestModel = { creatorId: creatorId };

    this.printRequestService.customerSubmitPrintRequest(printRequestId, submitPrintRequestModel)
      .subscribe(
        isUpdated => {
          if (isUpdated) {
            this.toastrService.success("Print request submitted successfully");
            this.refreshPrintRequests();
          } else {
            this.toastrService.error("Unknown error! Please try again");
          }
        },
        err => {
          this.toastrService.error("Unknown error! Please try again");
        });
  }
}
