import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleSelectionComponent } from './components/role-selection/role-selection.component';
import { CustomerRegistrationComponent } from './components/customer-registration/customer-registration.component';
import { CreatorRegistrationComponent } from './components/creator-registration/creator-registration.component';
import { CreatorProfileComponent } from './components/creator-profile/creator-profile.component';
import { PrintRequestCreationComponent } from './components/print-request-creation/print-request-creation.component';
import { CustomerPrintRequestsComponent } from './components/customer-print-requests/customer-print-requests.component';

const routes: Routes = [
  { path: 'role-selection', component: RoleSelectionComponent },
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'creator-registration', component: CreatorRegistrationComponent },
  { path: 'creator-profile', component: CreatorProfileComponent },
  { path: 'request-creation', component: PrintRequestCreationComponent },
  { path: 'customer-requests', component: CustomerPrintRequestsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
