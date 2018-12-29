import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import { ContactsListResolver } from './_resolvers/contacts-list.resolver';
import { ContactViewComponent } from './contact-view/contact-view.component';
import { ContactViewResolver } from './_resolvers/contact-view.resolver';
import { ContactEditComponent } from './contact-edit/contact-edit.component';
import { ContactEditResolver } from './_resolvers/contact-edit.resolver';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'contact/:id', component: ContactViewComponent, resolve: {contact: ContactViewResolver} },
  { path: 'contact/edit/:id', component: ContactEditComponent, resolve: {contact: ContactEditResolver} },
  { path: 'contacts', component: ContactsListComponent, resolve: {contacts: ContactsListResolver} },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
