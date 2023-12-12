import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';
import { CustomerRegistrationComponent } from './components/customer-registration/customer-registration.component';
import { CreatorRegistrationComponent } from './components/creator-registration/creator-registration.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { CreatorProfileComponent } from './components/creator-profile/creator-profile.component';
import { RoleSelectionComponent } from './components/role-selection/role-selection.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { CustomerPrintRequestCreationComponent } from './components/customer-print-request-creation/customer-print-request-creation.component';

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
    path: 'customer/register',
    component: CustomerRegistrationComponent
  },
  {
    path: 'customer/print-request-creation',
    component: CustomerPrintRequestCreationComponent
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
