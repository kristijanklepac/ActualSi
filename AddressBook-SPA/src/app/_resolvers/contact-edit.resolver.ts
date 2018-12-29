import {Injectable} from '@angular/core';
import {Contact} from '../_models/contact';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { ContactService } from '../_services/contact.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ContactEditResolver implements Resolve<Contact> {
    constructor(private contactService: ContactService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Contact> {
        return this.contactService.getContact(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving your data');
                this.router.navigate(['/contacts']);
                return of(null);
            })
        );
    }
}