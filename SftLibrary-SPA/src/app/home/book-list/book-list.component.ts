import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/_models/book';
import { BookService } from 'src/app/_services/book.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: Book[];

  constructor(private bookService: BookService, private alertify: AlertifyService) { }
  searchParam = '';

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getBooks(this.searchParam).subscribe((books: Book[]) => {
      this.books = books;
    }, error => {
      this.alertify.error(error);

    });
  }

  resetFilter() {
    this.searchParam = '';
    this.loadBooks();
  }

}
