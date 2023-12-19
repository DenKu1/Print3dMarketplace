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
  selector: 'creator-print-requests',
  templateUrl: './creator-print-requests.component.html',
  styleUrls: ['./creator-print-requests.component.css']
})
export class CreatorPrintRequestsComponent {
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

    if (this.currentUser.isCreator == false) {
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
      printRequests: this.printRequestService.getCreatorPrintRequests()
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

  downloadSTLScheme(printRequestId: string, printRequestOwnerId: string): void {
    this.printRequestService.creatorDownloadStlScheme(printRequestId, printRequestOwnerId).subscribe(
      model => {
        if (!model || !model.data || !model.fileName) {
          this.toastrService.error("Error! File not found");
        }
        else {
          const blob = this.dataURItoBlob(model.data);
          const url = window.URL.createObjectURL(blob);

          var anchor = document.createElement("a");
          anchor.download = model.fileName;
          anchor.href = url;
          anchor.click();
        }
      },
      err => {
        this.toastrService.error("Unknown error! Please try again");
      });
  }

  dataURItoBlob(dataURI: string): Blob {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);

    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }

    const blob = new Blob([int8Array], { type: 'file/stl' });

    return blob;
  }

  modifyModelTitle(fileName: string): string {
    if (!fileName) {
      return null;
    }

    var fileExtension = fileName.split('.').slice(-1)[0];

    if (fileName.length > 20)
      return fileName.substring(0, 20) + "..." + fileExtension;

    return fileName;
  }

  getMaterialName(id: string): string {
    return this.templateMaterials.find(material => material.id === id)?.name
  }

  isCurrentCreatorNotYetSubmitted(
    printRequest: PrintRequestModel): boolean {
    let newOrSubStatus: boolean = printRequest.printRequestStatusName === 'New' || printRequest.printRequestStatusName === 'CreatorSubmission';
    let alredySubmitted: boolean = printRequest.submittedCreators && printRequest.submittedCreators.some(creator => creator.creatorId === this.currentUser.id);

    return newOrSubStatus && !alredySubmitted;
  }

  isCurrentCreatorAlreadySubmitted(printRequest: PrintRequestModel): boolean {
    let newOrSubStatus: boolean = printRequest.printRequestStatusName === 'New' || printRequest.printRequestStatusName === 'CreatorSubmission';
    let alredySubmitted: boolean = printRequest.submittedCreators && printRequest.submittedCreators.some(creator => creator.creatorId === this.currentUser.id);

    return newOrSubStatus && alredySubmitted;
  }

  isCustomerSubmittedForCurrentCreator(printRequest: PrintRequestModel): boolean {
    let submittedByCustomer: boolean = printRequest.printRequestStatusName === 'CustomerSubmission';
    let customerSubmittedCurrentUser: boolean = printRequest.customerSubmittedCreatorId === this.currentUser.id;

    return submittedByCustomer && customerSubmittedCurrentUser;
  }

  submitPrintRequest(id: string): void {
    this.printRequestService.creatorSubmitPrintRequest(id)
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
