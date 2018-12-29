import {Injectable} from '@angular/core';
import {Contact} from '../_models/contact';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { ContactService } from '../_services/contact.service';
//import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ContactsListResolver implements Resolve<Contact[]> {
    pageNumber = 1;
    pageSize = 12;

    constructor(private contactService: ContactService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Contact[]> {
        return this.contactService.getContacts(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                console.log('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}