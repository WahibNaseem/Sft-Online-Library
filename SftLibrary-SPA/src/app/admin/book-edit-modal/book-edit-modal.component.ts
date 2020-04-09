import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PatronService } from 'src/app/_services/patron.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Book } from 'src/app/_models/book';
import { Patron } from 'src/app/_models/patron';
import { BookService } from 'src/app/_services/book.service';
import { Router } from '@angular/router';
import { CheckoutBookHistory } from 'src/app/_models/checkoutBookHistory';

@Component({
  selector: 'app-book-edit-modal',
  templateUrl: './book-edit-modal.component.html',
  styleUrls: ['./book-edit-modal.component.css']
})
export class BookEditModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  book: Book;
  checkoutBookHistory: CheckoutBookHistory;

  // tslint:disable-next-line: max-line-length
  constructor(
    public bsModalRef: BsModalRef,
    private patronService: PatronService,
    private alertify: AlertifyService,
    private bookService: BookService,
    private router: Router) { }
  value: any;
  model: any;
  message: string;
  bookStatus = false;
  ngOnInit() {
    this.getBookStatus();
    this.getBook(this.book.id);
  }

  getPatron() {
    this.patronService.getPatron(this.value).subscribe(response => {
      this.model = response;
      this.message = 'Patron ' + this.model.userName + ' Found !';
    }, error => {
      this.alertify.error('Faild to find user');
    });
  }

  getBookStatus() {
    if (this.book.status.name === 'Checked Out') {
      this.bookStatus = true;
    }
  }

  getBook(id: number) {
    this.bookService.getBook(id).subscribe(response => {
      this.checkoutBookHistory = response;
    }, error => {
      this.alertify.error(error);
    });
  }

  checkOutItem(id, bookId) {
    this.bookService.checkOutItem(id, bookId).subscribe((response) => {
      this.alertify.success('Book CheckOut Successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.bsModalRef.hide();
      this.router.navigate(['/home']);

    });
  }

  checkInItem(id) {
    this.bookService.checkInItem(id).subscribe((response) => {
      this.alertify.success('Book CheckIn Successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.bsModalRef.hide();
      this.router.navigate(['/home']);
    });
  }

  clearMessage() {
    this.message = '';
  }



}
