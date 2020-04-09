import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, ObservableInput } from 'rxjs';
import { Book } from '../_models/book';
import { environment } from 'src/environments/environment';
import { CheckoutBookHistory } from '../_models/checkoutBookHistory';

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
    return this.client.get<Book[]>(this.baseUrl + 'books/books?search=' + search, httpOptions);
  }

  getBook(id: number): Observable<CheckoutBookHistory> {
    return this.client.get<CheckoutBookHistory>(this.baseUrl + 'books/' + id, httpOptions);
  }

  saveBook(book: Book) {
    return this.client.post(this.baseUrl + 'books', book, httpOptions);
  }

  removeBook(id: number) {
    return this.client.delete(this.baseUrl + 'books/' + id, httpOptions);
  }

  checkOutItem(id: number, bookId: number) {
    return this.client.post(this.baseUrl + 'books/' + id + '/checkout/' + bookId, httpOptions);
  }

  checkInItem(bookId: number) {
    return this.client.post(this.baseUrl + 'books/checkinItem/' + bookId, httpOptions);
  }

}
