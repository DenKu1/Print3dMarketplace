import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { PrinterModel } from '../models/printer/printerModel';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../models/common/responseModel';
import { TemplatePrinterModel } from '../models/printer/templatePrinterModel';
import { NozzleModel } from '../models/printer/nozzleModel';

@Injectable({
  providedIn: 'root'
})
export class PrinterService {
  constructor(private http: HttpClient)
  {
  }

  getAllCreatorPrinters(userId: string): Observable<PrinterModel[]> {
    return this.http.get<ResponseModel>(`${environment.printerApiUrl}/printers/${userId}`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as PrinterModel[];
        } else {
          return null;
        }
      }));
  }

  getAllTemplatePrinters(): Observable<TemplatePrinterModel[]> {
    return this.http.get<ResponseModel>(`${environment.printerApiUrl}/template-printers`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as TemplatePrinterModel[];
        } else {
          return null;
        }
      }));
  }

  getAllNozzles(): Observable<NozzleModel[]> {
    return this.http.get<ResponseModel>(`${environment.printerApiUrl}/nozzles`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as NozzleModel[];
        } else {
          return null;
        }
      }));
  }

  updateCreatorPrinters(userId: string, printers: PrinterModel[]): Observable<boolean> {
    return this.http.put<ResponseModel>(`${environment.printerApiUrl}/printers/${userId}`, printers)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }
}

