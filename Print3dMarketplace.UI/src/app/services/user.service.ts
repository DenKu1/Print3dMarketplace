import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { User } from '../models/user/user';

@Injectable({ providedIn: 'root' })
export class UserService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser') || ''));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

/*  login(userName: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/login`, { userName, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  register(userName: string, email: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/register`, { userName, email, password });
  }*/
}
