import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';


import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';

import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { PatronService } from './_services/patron.service';
import { BookService } from './_services/book.service';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { PatronListResolver } from './_resolvers/patron-list.resolver';


import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { PatronManagementComponent } from './admin/patron-management/patron-management.component';
import { BookManagementComponent } from './admin/book-management/book-management.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { BookListComponent } from './home/book-list/book-list.component';
import { BookCardComponent } from './home/book-card/book-card.component';
import { BookModalComponent } from './admin/book-modal/book-modal.component';
import { PatronRolesModalComponent } from './admin/patron-roles-modal/patron-roles-modal.component';
import { PatronProfileComponent } from './admin/patron-profile/patron-profile.component';







export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent,
      RegisterComponent,
      LoginComponent,
      PatronManagementComponent,
      BookManagementComponent,
      AdminPanelComponent,
      BookListComponent,
      BookCardComponent,
      BookModalComponent,
      PatronRolesModalComponent,
      PatronProfileComponent,
      HasRoleDirective

   ],
   entryComponents: [
      BookModalComponent,
      PatronRolesModalComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot(),
      ModalModule.forRoot(),
      BrowserAnimationsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard,
      RoleGuard,
      ErrorInterceptorProvider,
      PatronService,
      BookService,
      PatronListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
