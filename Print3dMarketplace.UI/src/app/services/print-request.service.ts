import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../models/common/responseModel';
import { CreatePrintRequestModel } from '../models/print-requests/createPrintRequestModel';
import { PrintRequestModel } from '../models/print-requests/printRequestModel';
import { SubmitPrintRequestModel } from '../models/print-requests/submitPrintRequestModel';
import { FileResponseModel } from '../models/print-requests/fileResponse';

@Injectable({
  providedIn: 'root'
})
export class PrintRequestService {
  constructor(private http: HttpClient)
  {
  }
  
  getCustomerPrintRequests(): Observable<PrintRequestModel[]> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/customer/print-requests`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as PrintRequestModel[];
        } else {
          return null;
        }
      }));
  }

  getCreatorPrintRequests(): Observable<PrintRequestModel[]> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/creator/print-requests`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as PrintRequestModel[];
        } else {
          return null;
        }
      }));
  }

  createPrintRequest(printRequest: CreatePrintRequestModel): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/customer/print-requests/`, printRequest)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }

  customerDownloadStlScheme(printRequestId: string): Observable<FileResponseModel> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/customer/print-requests/${printRequestId}/download`)
    .pipe(map(response => {
      if (response.isSuccess) {
        return response.result as FileResponseModel;
      } else {
        return null;
      }
    }));
  }

  creatorDownloadStlScheme(printRequestId: string, printRequestOwnerId: string): Observable<FileResponseModel> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/creator/print-requests/${printRequestId}/download/${printRequestOwnerId}`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as FileResponseModel;
        } else {
          return null;
        }
      }));
  }

  cancelPrintRequest(printRequestId: string): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/customer/print-requests/${printRequestId}/cancel`, null)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }

  creatorSubmitPrintRequest(printRequestId: string): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/creator/print-requests/${printRequestId}/submit`, null)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }

  customerSubmitPrintRequest(printRequestId: string, submitPrintRequestModel: SubmitPrintRequestModel): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/customer/print-requests/${printRequestId}/submit`, submitPrintRequestModel)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }
}
