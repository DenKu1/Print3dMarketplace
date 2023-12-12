import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { MaterialModel } from '../models/material/materialModel';
import { environment } from '../../enviroments/environment';
import { ResponseModel } from '../models/common/responseModel';
import { TemplateMaterialModel } from '../models/material/templateMaterialModel';
import { ColorModel } from '../models/material/colorModel';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  constructor(private http: HttpClient)
  {
  }

  getAllCreatorMaterials(userId: string): Observable<MaterialModel[]> {
    return this.http.get<ResponseModel>(`${environment.materialApiUrl}/materials/${userId}`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as MaterialModel[];
        } else {
          return null;
        }
      }));
  }

  getAllTemplateMaterials(): Observable<TemplateMaterialModel[]> {
    return this.http.get<ResponseModel>(`${environment.materialApiUrl}/template-materials`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as TemplateMaterialModel[];
        } else {
          return null;
        }
      }));
  }

  getAllColors(): Observable<ColorModel[]> {
    return this.http.get<ResponseModel>(`${environment.materialApiUrl}/colors`)
      .pipe(map(response => {
        if (response.isSuccess) {
          return response.result as ColorModel[];
        } else {
          return null;
        }
      }));
  }

  updateCreatorMaterials(userId: string, materials: MaterialModel[]): Observable<boolean> {
    return this.http.put<ResponseModel>(`${environment.materialApiUrl}/materials/${userId}`, materials)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }
}

