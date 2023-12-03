import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../../models/user/userModel';
import { environment } from '../../enviroments/environment';
import { CreatorRegistrationRequestModel } from '../../models/user/creatorRegistrationRequestModel';
import { CustomerRegistrationRequestModel } from '../../models/user/customerRegistrationRequestModel';

@Injectable({ providedIn: 'root' })
export class UserService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(userName: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/login`, { userName, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  registerCustomer(customerRegistrationModel: CustomerRegistrationRequestModel) {
    return this.http.post<any>(`${environment.authApiUrl}/customer/register`, customerRegistrationModel);
  }

  registerCreator(creatorRegistrationModel: CreatorRegistrationRequestModel) {
    return this.http.post<any>(`${environment.authApiUrl}/creator/register`, creatorRegistrationModel);
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/users/`);
  }

  getUserById(id: number) {
    return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
  }

  getUserByUserName(userName: string) {
    return this.http.get<User>(`${environment.apiUrl}/users/by-user-name/${userName}`);
  }

  deleteUser(id: number) {
    return this.http.delete(`${environment.apiUrl}/users/${id}`);
  }

  attachUserTags(tags: string[]) {
    return this.http.post(`${environment.apiUrl}/users/${this.currentUserSubject.value.id}/attachTags`, tags,{headers: this.headers})
  }

  detachUserTag(tag: string) {
    return this.http.post(`${environment.apiUrl}/users/${this.currentUserSubject.value.id}/detachTag`, `"${tag}"`,{headers: this.headers})
  }
}
