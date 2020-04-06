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
  baseUrl = environment.apiUrl + 'admin/usersWithRoles';

  constructor(private client: HttpClient) { }


  getPatrons(): Observable<User[]> {
    return this.client.get<User[]>(this.baseUrl, httpOptions);
  }

}
