import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  contactMode = false;

  constructor() { }

  ngOnInit() {
  }

  contactToggle() {
    this.contactMode = true;
  }

  cancelEventMode(cancelEvent: boolean) {
    this.contactMode = cancelEvent;
  }

}
