import { Component, OnInit } from '@angular/core';
import { Contact } from '../_models/contact';
import { ContactService } from '../_services/contact.service';
import { AlertifyService } from '../_services/alertify.service';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-contact-view',
  templateUrl: './contact-view.component.html',
  styleUrls: ['./contact-view.component.css']
})
export class ContactViewComponent implements OnInit {
  contact: Contact;

  constructor(private contactService: ContactService, private route: ActivatedRoute, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.contact = data['contact'];
    });
  }

  deleteContact() {
    this.alertify.confirm('Are you sure you want to delete this contact?', () => {
      this.contactService.deleteContact(this.contact.id).subscribe(() => {  
        this.alertify.success('Contact has been deleted');
      }, error => {
        this.alertify.error('Failed to delete contact');
      }, () => {
          this.router.navigate(['/contacts']);
        });
    });
  }

}
