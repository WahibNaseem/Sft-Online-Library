import { Routes } from '@angular/router';


import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

export const appRoutes: Routes = [

    { path: 'login', component: LoginComponent },
    { path: 'regiser', component: RegisterComponent },
    { path: '**' , redirectTo: 'login', pathMatch: 'full' }
];
