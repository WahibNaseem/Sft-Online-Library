import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import { User } from '../_models/user';
import { PatronService } from '../_services/patron.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class PatronListResolver implements Resolve<User[]> {

    constructor(private patronService: PatronService, private alertify: AlertifyService, private router: Router) { }

    resolve(): Observable<User[]> {
        return this.patronService.getPatrons().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
