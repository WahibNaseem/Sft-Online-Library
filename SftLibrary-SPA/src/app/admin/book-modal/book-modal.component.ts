import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Book } from 'src/app/_models/book';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookService } from 'src/app/_services/book.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-book-modal',
  templateUrl: './book-modal.component.html',
  styleUrls: ['./book-modal.component.css']
})
export class BookModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  list: any[] = [];
  newBook: Book;
  newBookForm: FormGroup;
  model: any = {};


  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder,
    private bookService: BookService, private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
    this.createNewBookForm();
  }

  createNewBookForm() {
    this.newBookForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
      author: ['', [Validators.required, Validators.maxLength(25)]],
      year: ['', Validators.required]
    });
  }

  addNewBook() {
    if (this.newBookForm.valid) {
      this.newBook = Object.assign({}, this.newBookForm.value);
      this.bookService.saveBook(this.newBook).subscribe(() => {
        this.alertify.success('Book Saveed Successfully');
        this.bsModalRef.hide();
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/home']);
      });


    }

  }

}
