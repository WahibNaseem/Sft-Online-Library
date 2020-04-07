import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole as Array<string>;
    if (expectedRole) {
      const match = this.authService.roleMatch(expectedRole);
      if (match) {
        return true;
      } else {
        this.router.navigate(['/home']);
        // //to do message
        // this.alertify.error('You are not allowed to access this area');
        return false;
      }
    }
    // //to do message
    // this.alertify.error('You shall not pass');
    this.router.navigate(['/home']);
    return false;
  }
}
