import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';
import { CustomerRegistrationComponent } from './components/customer-registration/customer-registration.component';
import { CreatorRegistrationComponent } from './components/creator-registration/creator-registration.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { CreatorProfileComponent } from './components/creator-profile/creator-profile.component';
import { RoleSelectionComponent } from './components/role-selection/role-selection.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { CustomerPrintRequestCreationComponent } from './components/customer-print-request-creation/customer-print-request-creation.component';
import { CustomerPrintRequestsComponent } from './components/customer-print-requests/customer-print-requests.component';
import { CreatorPrintRequestsComponent } from './components/creator-print-requests/creator-print-requests.component';

const routes: Routes = [
  {
    path: '',
    component: RoleSelectionComponent
  },
  {
    path: 'user/login',
    component: UserLoginComponent
  },
  {
    path: 'user/select-role',
    component: RoleSelectionComponent
  },
  {
    path: 'creator/register',
    component: CreatorRegistrationComponent
  },
  {
    path: 'creator/profile',
    component: CreatorProfileComponent
  },
  {
    path: 'creator/print-requests',
    component: CreatorPrintRequestsComponent
  },
  {
    path: 'customer/register',
    component: CustomerRegistrationComponent
  },
  {
    path: 'customer/print-request-creation',
    component: CustomerPrintRequestCreationComponent
  },
  {
    path: 'customer/print-requests',
    component: CustomerPrintRequestsComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
