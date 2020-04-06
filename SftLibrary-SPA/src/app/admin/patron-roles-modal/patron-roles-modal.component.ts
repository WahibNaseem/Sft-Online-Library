import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-patron-roles-modal',
  templateUrl: './patron-roles-modal.component.html',
  styleUrls: ['./patron-roles-modal.component.css']
})
export class PatronRolesModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  list: any[] = [];


  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

}
