import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
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
  patrons: User[];
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
    this.patronService.getPatrons().subscribe((patrons: User[]) => {
      this.patrons = patrons;
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });
  }


  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user),
      title: 'Edit Roles for'
    };
    this.bsModalRef = this.modalService.show(PatronRolesModalComponent, { initialState });
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter((el: any) => el.checked === true).map((el: any) => el.name)]
      };
      if (rolesToUpdate) {
        console.log(rolesToUpdate);
        this.patronService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
        }, error => {
          this.alertify.error(error);
        });
      }
    });
  }



  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Member', value: 'Member' },
    ];

    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      // tslint:disable-next-line: prefer-for-of
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
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
