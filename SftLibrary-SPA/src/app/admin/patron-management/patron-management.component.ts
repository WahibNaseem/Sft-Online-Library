import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { PatronService } from 'src/app/_services/patron.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PatronRolesModalComponent } from '../patron-roles-modal/patron-roles-modal.component';

@Component({
  selector: 'app-patron-management',
  templateUrl: './patron-management.component.html',
  styleUrls: ['./patron-management.component.css']
})
export class PatronManagementComponent implements OnInit {
  patrons: User[];
  bsModalRef: BsModalRef;

  constructor(private patronService: PatronService, private alertify: AlertifyService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getPartrons();
  }

  getPartrons() {
    this.patronService.getPatrons().subscribe((patrons: User[]) => {
      this.patrons = patrons;
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });
  }


  editRolesModal() {
    const initialState = {
      list: [
        'Open a modal with component',
        'Pass your data',
        'Do something else',
        '...'
      ],
      title: 'Edit Patron Roles'
    };
    this.bsModalRef = this.modalService.show(PatronRolesModalComponent, { initialState });
    this.bsModalRef.content.closeBtnName = 'Close';
  }



}
