import { Routes } from '@angular/router';


import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AuthGuard } from './_guards/auth.guard';
import { BookListComponent } from './home/book-list/book-list.component';
import { PatronProfileComponent } from './admin/patron-profile/patron-profile.component';

// export const appRoutes: Routes = [

//     { path: 'home', component: BookListComponent },
//     { path: 'login', component: LoginComponent },
//     { path: 'register', component: RegisterComponent },



//     {
//         path: '',
//         runGuardsAndResolvers: 'always',
//         canActivate: [AuthGuard],
//         children: [
//             {path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin', 'Moderator']}},
//             {
//                 path: 'patron', component: PatronProfileComponent
//             },
//         ]
//     },

//     { path: '**', redirectTo: 'home', pathMatch: 'full' },
// ];




export const appRoutes: Routes = [
    { path: 'home', component: BookListComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [

            { path: 'admin', component: AdminPanelComponent, data: { id: '1', name: 'Angular' } },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];



