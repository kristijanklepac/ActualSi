import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ContactService } from '../_services/contact.service';
import { AlertifyService } from '../_services/alertify.service';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  @Output() cancelEvent = new EventEmitter();
  model: any = {};
  intlObj: any;
  telBrValid: string;
  


  constructor(private contactService: ContactService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  addContact() {
    
    this.contactService.addContact(this.model).subscribe((response) => {
      this.alertify.success("Contact is added to Address Book");
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/contacts']);
    });
  }

  cancelContact() {
    this.cancelEvent.emit(false);
    //console.log('canceled');
    this.alertify.warning("Canceled button pressed.");
  }

  telInputObject(obj) {
    this.intlObj = obj;
  }
  getNumber(obj) {
    console.log('"getme"', obj);
    this.model.telephoneNumber = obj;
  }
  onCountryChange(obj) {
    if ( this.intlObj ) {
      this.intlObj.intlTelInput('setNumber', '');
    }
  }
  hasError(obj) {
    console.log(obj);
    if (obj) {
       console.log('Telefonski broj je ispravan');
       this.telBrValid = 'Valid';
    } else {
      console.log('Telefonski broj je neispravan');
      this.telBrValid = 'Not Valid';
    }
  }

}
