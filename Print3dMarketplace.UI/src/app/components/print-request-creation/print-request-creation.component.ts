import { Component, OnInit } from '@angular/core';
import { faInfoCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'print-request-creation',
  templateUrl: './print-request-creation.component.html',
  styleUrls: ['./print-request-creation.component.css']
})
export class PrintRequestCreationComponent implements OnInit {
  faInfoCircle = faInfoCircle;

  ngOnInit() {
  }
}
