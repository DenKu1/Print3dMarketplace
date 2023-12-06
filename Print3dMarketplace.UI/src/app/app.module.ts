import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { CustomerRegistrationComponent } from './components/customer-registration/customer-registration.component';
import { CreatorRegistrationComponent } from './components/creator-registration/creator-registration.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { CreatorProfileComponent } from './components/creator-profile/creator-profile.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CustomerRegistrationComponent,
    CreatorRegistrationComponent,
    UserLoginComponent,
    CreatorProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
