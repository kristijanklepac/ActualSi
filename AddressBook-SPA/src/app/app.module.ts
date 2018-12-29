import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TruncateModule } from 'ng2-truncate';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { ContactService } from './_services/contact.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { Ng2TelInputModule } from 'ng2-tel-input';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import { ContactsListResolver } from './_resolvers/contacts-list.resolver';
import { ContactViewResolver } from './_resolvers/contact-view.resolver';
import { ContactEditResolver } from './_resolvers/contact-edit.resolver';
import { PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { ContactEditComponent } from './contact-edit/contact-edit.component';
import { ContactViewComponent } from './contact-view/contact-view.component';

@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    NavComponent,
    HomeComponent,
    ContactComponent,
    ContactsListComponent,
    ContactEditComponent,
    ContactViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2TelInputModule,
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    TruncateModule
  ],
  providers: [
    ContactService, 
    ErrorInterceptorProvider, 
    ContactsListResolver, 
    ContactViewResolver, 
    ContactEditResolver, 
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
