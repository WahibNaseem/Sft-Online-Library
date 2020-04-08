import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../_models/book';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer' + localStorage.getItem('token')
  })
};
@Injectable({
  providedIn: 'root'
})
export class BookService {
  baseUrl = environment.apiUrl;

  constructor(private client: HttpClient) { }

  getBooks(search: string): Observable<Book[]> {
    return this.client.get<Book[]>(this.baseUrl + 'books/books?search=' + search , httpOptions);
  }

  saveBook(book: Book) {
    return this.client.post(this.baseUrl + 'books', book, httpOptions);
  }

  removeBook(id: number) {
    return this.client.delete(this.baseUrl + 'books/' + id, httpOptions);
  }

 

}
