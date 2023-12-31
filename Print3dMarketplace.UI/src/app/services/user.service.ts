import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../enviroments/environment';
import { CreatorRegistrationRequestModel } from '../models/user/creatorRegistrationRequestModel';
import { CustomerRegistrationRequestModel } from '../models/user/customerRegistrationRequestModel';
import { LoginRequestModel } from '../models/user/loginRequestModel';
import { LoginResponseModel } from '../models/user/loginResponseModel';
import { UserModel } from '../models/user/userModel';
import { ResponseModel } from '../models/common/responseModel';
import { CreatorModel } from '../models/user/creatorModel';

@Injectable({ providedIn: 'root' })
export class UserService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  private currentUserSubject: BehaviorSubject<UserModel>;
  private currentTokenSubject: BehaviorSubject<string>;

  public currentUser: Observable<UserModel>;
  public currentToken: Observable<string>;

  constructor(private http: HttpClient) {
    const storedUser = localStorage.getItem('currentUser');
    const parsedUser = storedUser && storedUser !== 'undefined' ? JSON.parse(storedUser) : null;
    this.currentUserSubject = new BehaviorSubject<UserModel>(parsedUser);
    this.currentUser = this.currentUserSubject.asObservable();

    const storedToken = localStorage.getItem('userToken');
    const parsedToken = storedToken && storedToken !== 'undefined' ? JSON.parse(storedToken) : null;
    this.currentTokenSubject = new BehaviorSubject<string>(parsedToken);
    this.currentToken = this.currentTokenSubject.asObservable();
  }

  public get currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  public get currentUserTokenValue(): string {
    return this.currentTokenSubject.value;
  }

  login(loginRequestModel: LoginRequestModel): Observable<UserModel> {
    return this.http.post<ResponseModel>(`${environment.authApiUrl}/login`, loginRequestModel)
      .pipe(map(response => {

        if (response.isSuccess) {
          const loginResponse = response.result as LoginResponseModel;

          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(loginResponse.user));
          localStorage.setItem('userToken', JSON.stringify(loginResponse.token));

          // init the subjects and observables
          this.currentUserSubject.next(loginResponse.user);
          this.currentTokenSubject.next(loginResponse.token);
          this.currentUser = this.currentUserSubject.asObservable();
          this.currentToken = this.currentTokenSubject.asObservable();

          return loginResponse.user;
        } else {
          throw new Error(response.message);
        }
      }));
  }

  registerCustomer(customerRegistrationModel: CustomerRegistrationRequestModel) {
    return this.http.post<ResponseModel>(`${environment.authApiUrl}/customer/register`, customerRegistrationModel)
      .pipe(map(response => {

        if (response.isSuccess) {
          return response.result;
        } else {
          throw new Error(response.message);
        }
      }));
  }

  registerCreator(creatorRegistrationModel: CreatorRegistrationRequestModel) {
    return this.http.post<ResponseModel>(`${environment.authApiUrl}/creator/register`, creatorRegistrationModel)
      .pipe(map(response => {

        if (response.isSuccess) {
          return response.result;
        } else {
          throw new Error(response.message);
        }
      }));
  }

  getCreator(userId: string): Observable<CreatorModel> {
    return this.http.get<ResponseModel>(`${environment.authApiUrl}/creator/${userId}`)
      .pipe(map(response => {

        if (response.isSuccess) {
          return response.result as CreatorModel;
        } else {
          return null;
        }
      }));
  }

  updateCreator(userId: string, creator: CreatorModel): Observable<boolean> {
    return this.http.put<ResponseModel>(`${environment.authApiUrl}/creator/${userId}`, creator)
      .pipe(map(response => {
        return response.isSuccess;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('userToken');

    // reset the subjects and observables
    this.currentUserSubject.next(null);
    this.currentTokenSubject.next(null);
    this.currentUser = this.currentUserSubject.asObservable();
    this.currentToken = this.currentTokenSubject.asObservable();
  }
}
