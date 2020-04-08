import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Book } from 'src/app/_models/book';
import { BookService } from 'src/app/_services/book.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BookModalComponent } from '../book-modal/book-modal.component';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { BookEditModalComponent } from '../book-edit-modal/book-edit-modal.component';

@Component({
  selector: 'app-book-management',
  templateUrl: './book-management.component.html',
  styleUrls: ['./book-management.component.css']
})
export class BookManagementComponent implements OnInit {
  books: Book[];
  bsModalRef: BsModalRef;

  // tslint:disable-next-line: max-line-length
  constructor(private alertify: AlertifyService, private bookService: BookService, private modalService: BsModalService, private router: Router) { }

  ngOnInit() {
    this.getBooks();
  }

  getBooks() {
    this.bookService.getBooks('').subscribe((books: Book[]) => {
      console.log(books);
      this.books = books;
    }, error => {
      this.alertify.error(error);
    });
  }

  addBookModal() {
    const initialState = {
      title: 'Add New Book'
    };
    this.bsModalRef = this.modalService.show(BookModalComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  editBookModal(book: Book) {
    const initialState = {
       book,
       title: 'Edit Book'
    };
    this.bsModalRef = this.modalService.show(BookEditModalComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  removeBook(id: number) {
    this.bookService.removeBook(id).subscribe(() => {
      this.alertify.success('Book Removed Successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

}
