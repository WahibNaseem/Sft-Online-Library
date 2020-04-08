import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Patron } from '../_models/patron';
import { Observable } from 'rxjs';


const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer' + localStorage.getItem('token')
  })
};
@Injectable({
  providedIn: 'root'
})
export class PatronService {
  baseUrl = environment.apiUrl + 'admin/';

  constructor(private client: HttpClient) { }


  getPatrons(): Observable<Patron[]> {
    return this.client.get<Patron[]>(this.baseUrl + 'usersWithRoles', httpOptions);
  }

  updateUserRoles(patron: Patron, roles: {}) {
    return this.client.post(this.baseUrl + 'EditRoles/' + patron.userName, roles, httpOptions);
  }


  getPatron(id: number) {
    return this.client.get(environment.apiUrl + 'users/' + id, httpOptions);
  }

}
