import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
   }

   logout() {
     localStorage.removeItem('token');
     localStorage.removeItem('user');
     this.alertify.success('Logged out successfully');
     this.router.navigate(['/home']);
   }

}
