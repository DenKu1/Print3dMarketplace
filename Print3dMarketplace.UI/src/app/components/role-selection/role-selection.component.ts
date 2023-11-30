import { Component } from '@angular/core';
import { faCircleArrowRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'role-selection',
  templateUrl: './role-selection.component.html',
  styleUrls: ['./role-selection.component.css']
})
export class RoleSelectionComponent {
  faCircleArrowRight = faCircleArrowRight;
}
