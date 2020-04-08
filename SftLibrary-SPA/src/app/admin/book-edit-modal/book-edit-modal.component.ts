import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PatronService } from 'src/app/_services/patron.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-book-edit-modal',
  templateUrl: './book-edit-modal.component.html',
  styleUrls: ['./book-edit-modal.component.css']
})
export class BookEditModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  list: any[] = [];
  constructor(public bsModalRef: BsModalRef, private patronService: PatronService, private alertify: AlertifyService) { }
  value = 0;
  model: any;
  found = false;
  ngOnInit() {
  }

  getPatron() {
    this.patronService.getPatron(this.value).subscribe(response => {      
      this.model = response;
      this.found = true;
    }, error => {
      this.alertify.error('Faild to find user');
    });
  }

}
