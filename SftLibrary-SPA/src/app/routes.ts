import { Routes } from '@angular/router';


import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AuthGuard } from './_guards/auth.guard';
import { BookListComponent } from './home/book-list/book-list.component';
import { PatronProfileComponent } from './admin/patron-profile/patron-profile.component';
import { RoleGuard } from './_guards/role.guard';
import { PatronListResolver } from './_resolvers/patron-list.resolver';


export const appRoutes: Routes = [

    {
        path: '',
        component: BookListComponent
    },

    {
        path: 'login',
        component: LoginComponent
    },

    {
        path: 'patron', component: PatronProfileComponent,
        canActivate: [AuthGuard]
    },

    { path: 'register', component: RegisterComponent },

    {
        path: 'admin',
        component: AdminPanelComponent,
        canActivate: [RoleGuard],
        data: {
            expectedRole: ['Admin', 'Moderator']
        }
    },

    { path: '**', redirectTo: '', pathMatch: 'full' },
];



