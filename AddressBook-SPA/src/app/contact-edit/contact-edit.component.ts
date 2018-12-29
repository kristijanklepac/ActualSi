import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { ContactService } from '../_services/contact.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
import { Router } from '@angular/router';
import { Contact } from '../_models/contact';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit {
  @Output() cancelEvent = new EventEmitter();
  @ViewChild('editForm') editForm: NgForm;
  @ViewChild('input1') inputEl: ElementRef;
  contact: Contact;
  intlObj: any;
  telBrValid: string;
  model: any = {};

  validNr=true;

  constructor(private contactService: ContactService, private router: Router, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.contact = data['contact'];
    });
    //console.log(this.contact.telephoneNumber);
  }

  updateContact() {
    //console.log(this.contact);
    this.contactService.updateContact( this.contact.id, this.contact ).subscribe(next => {
      this.alertify.success('Contact updated successfully');
      this.editForm.reset(this.contact);
    }, error => {
      this.alertify.error(error);
    });
  }

  cancelContact() {
    //console.log('canceled');
    this.alertify.warning("Canceled button pressed. Redirect to all contacts");
    this.router.navigate(['/contacts']);
  }

  telInputObject(obj) {
    //this.intlObj = obj;
    //console.log('obj', obj, this.contact.telephoneNumber.toString());
    obj.intlTelInput('setNumber', this.contact.telephoneNumber.toString());
  }
  getNumber(obj) {
    //console.log('"getme"', obj);
    this.contact.telephoneNumber = obj;
  }
  onCountryChange(obj) {
      //this.intlObj.intlTelInput('setNumber', this.contact.telephoneNumber.toString());
      this.editForm.control.markAsTouched();
      this.editForm.control.markAsDirty();
  }
  hasError(obj) {
    //console.log(obj);
    if (obj) {
       //console.log('Telefonski broj je ispravan');
       this.telBrValid = 'Valid';
       setTimeout(() => this.inputEl.nativeElement.focus());
       this.editForm.control.markAsDirty();
       this.validNr = true;
    } else {
      //console.log('Telefonski broj je neispravan');
      this.telBrValid = 'Not Valid';
      this.validNr = false;
    }
  }

}
