import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Patron } from '../_models/patron';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  patron: Patron;
  registerForm: FormGroup;

  constructor(private alertify: AlertifyService, private fb: FormBuilder, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmpassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });

  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : { 'mismatch': true };
  }

  register() {
    if (this.registerForm.valid) {
      this.patron = Object.assign({}, this.registerForm.value);
      this.authService.register(this.patron).subscribe(() => {
        this.alertify.success('Registered Successfully');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.patron).subscribe(() => {
          this.router.navigate(['/home']);

        });
      });
    }
  }
}
