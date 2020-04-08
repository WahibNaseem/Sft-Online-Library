import { Component, OnInit } from '@angular/core';
import { Patron } from 'src/app/_models/patron';
import { PatronService } from 'src/app/_services/patron.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PatronRolesModalComponent } from '../patron-roles-modal/patron-roles-modal.component';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-patron-management',
  templateUrl: './patron-management.component.html',
  styleUrls: ['./patron-management.component.css']
})
export class PatronManagementComponent implements OnInit {
  patrons: Patron[];
  bsModalRef: BsModalRef;

  // tslint:disable-next-line: max-line-length
  constructor(private patronService: PatronService, private route: ActivatedRoute, private alertify: AlertifyService, private modalService: BsModalService) { }

  ngOnInit() {
    // this.route.data.subscribe(data => {
    //   this.patrons = data.patrons.result;
    // });
    this.getPartrons();
  }

  getPartrons() {
    this.patronService.getPatrons().subscribe((patrons: Patron[]) => {
      this.patrons = patrons;
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });
  }


  editRolesModal(patron: Patron) {
    const initialState = {
      patron,
      roles: this.getRolesArray(patron),
      title: 'Edit Roles for'
    };
    this.bsModalRef = this.modalService.show(PatronRolesModalComponent, { initialState });
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter((el: any) => el.checked === true).map((el: any) => el.name)]
      };
      if (rolesToUpdate) {
        console.log(rolesToUpdate);
        this.patronService.updateUserRoles(patron, rolesToUpdate).subscribe(() => {
          patron.roles = [...rolesToUpdate.roleNames];
        }, error => {
          this.alertify.error(error);
        });
      }
    });
  }



  private getRolesArray(patron) {
    const roles = [];
    const patronRoles = patron.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Member', value: 'Member' },
    ];

    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      // tslint:disable-next-line: prefer-for-of
      for (let j = 0; j < patronRoles.length; j++) {
        if (availableRoles[i].name === patronRoles[j]) {
          isMatch = true,
            availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }

}
