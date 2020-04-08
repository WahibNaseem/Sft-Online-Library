import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
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


  getPatrons(): Observable<User[]> {
    return this.client.get<User[]>(this.baseUrl + 'usersWithRoles', httpOptions);
  }

  updateUserRoles(user: User, roles: {}) {
    return this.client.post(this.baseUrl + 'EditRoles/' + user.userName, roles, httpOptions);
  }


  getPatron(id: number) {
    return this.client.get(environment.apiUrl + 'users/' + id, httpOptions);
  }

}
