import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../models/common/responseModel';
import { CreatePrintRequestModel } from '../models/print-requests/createPrintRequestModel';
import { PrintRequestModel } from '../models/print-requests/printRequestModel';

@Injectable({
  providedIn: 'root'
})
export class PrintRequestService {
  constructor(private http: HttpClient)
  {
  }
  
  getCustomerPrintRequests(): Observable<PrintRequestModel[]> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as PrintRequestModel[];
        } else {
          return null;
        }
      }));
  }

  createPrintRequest(printRequest: CreatePrintRequestModel): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests/`, printRequest)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }

  cancelPrintRequest(printRequestId: string): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests/${printRequestId}/cancel`, null)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }
}
