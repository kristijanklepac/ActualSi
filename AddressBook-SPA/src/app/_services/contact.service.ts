import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Contact } from '../_models/contact';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addContact(model: any) {
    return this.http.post(this.baseUrl + 'address', model);
  }

  getContact(id): Observable<Contact> {
    return this.http.get<Contact>(this.baseUrl + 'address/' + id);
  }

  updateContact(id: number, contact: Contact) {
    return this.http.put(this.baseUrl + 'address/' + id, contact);
  }

  getContacts(page?, itemsPerPage?, searchText?): Observable<PaginatedResult<Contact[]>>  {
    const paginatedResult: PaginatedResult<Contact[]> = new PaginatedResult<Contact[]>();
    
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (searchText != null) {
      params = params.append('searchText', searchText);
    }
    
    return this.http.get<Contact[]>(this.baseUrl + 'address', { observe: 'response', params})
    .pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  deleteContact(id: number) {
    return this.http.post(this.baseUrl + 'address/' + id, {});
  }
}

