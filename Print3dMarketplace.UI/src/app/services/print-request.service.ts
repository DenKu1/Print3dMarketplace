import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../models/common/responseModel';
import { CreatePrintRequestModel } from '../models/print-requests/printRequestModel';

@Injectable({
  providedIn: 'root'
})
export class PrintRequestService {
  constructor(private http: HttpClient)
  {
  }
  
  getAllPrintRequests(): Observable<CreatePrintRequestModel[]> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as CreatePrintRequestModel[];
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
}
