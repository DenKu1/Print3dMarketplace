import { Component } from '@angular/core';
import { faMapMarker, faPhone, faUser } from '@fortawesome/free-solid-svg-icons';
import { faCircleArrowRight, faEnvelope, faLock } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'creator-registration',
  templateUrl: './creator-registration.component.html',
  styleUrls: ['./creator-registration.component.css']
})
export class CreatorRegistrationComponent {
  faUser = faUser;
  faEnvelope = faEnvelope;
  faLock = faLock;
  faCircleArrowRight = faCircleArrowRight;
  faMapMarker = faMapMarker;
  faPhone = faPhone;
}
