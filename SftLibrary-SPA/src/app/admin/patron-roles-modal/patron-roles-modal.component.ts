import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Patron } from 'src/app/_models/patron';

@Component({
  selector: 'app-patron-roles-modal',
  templateUrl: './patron-roles-modal.component.html',
  styleUrls: ['./patron-roles-modal.component.css']
})
export class PatronRolesModalComponent implements OnInit {
  @Output() updateSelectedRoles = new EventEmitter();
  title: string;
  patron: Patron;
  roles: any[];


  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

  updateRoles() {
    this.updateSelectedRoles.emit(this.roles);
    this.bsModalRef.hide();
  }

}
