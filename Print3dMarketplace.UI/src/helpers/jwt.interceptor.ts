import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from '../app/services/user.service';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: UserService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let currentUser = this.authenticationService.currentUserValue;
    let currentToken = this.authenticationService.currentUserTokenValue;

    if (currentUser && currentToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentToken}`
        }
      });
    }

    return next.handle(request);
  }
}
