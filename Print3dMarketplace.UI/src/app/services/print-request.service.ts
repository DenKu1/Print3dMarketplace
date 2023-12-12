import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../../models/common/responseModel';
import { PrintRequestModel } from '../../models/print-requests/printRequestModel';

@Injectable({
  providedIn: 'root'
})
export class PrintRequestService {
  constructor(private http: HttpClient)
  {
  }
  
  getAllPrintRequests(): Observable<PrintRequestModel[]> {
    return this.http.get<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as PrintRequestModel[];
        } else {
          return null;
        }
      }));
  }

  createPrintRequest(userId: string, materials: PrintRequestModel[]): Observable<boolean> {
    return this.http.post<ResponseModel>(`${environment.printRequestsApiUrl}/print-requests/${userId}`, materials)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }
}

