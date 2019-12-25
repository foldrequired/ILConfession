import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { User } from '../_models/user';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  registerForm: FormGroup;

  constructor(private authService: AuthService, private toastrService: ToastrService,
              private router: Router, private formBuilder: FormBuilder ) { }

  ngOnInit() {
    this.registerFormF();
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
    }
    this.authService.register(this.user).subscribe(() => {
      this.toastrService.success('Register successful');
    }, err => {
      this.toastrService.error(err);
    }, () => {
      this.authService.login(this.user).subscribe(() => {
        this.router.navigate(['/']);
      });
    });
  }

  registerFormF() {
    this.registerForm = this.formBuilder.group({
      gender: [''],
      email: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      country: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    },
    {validators: this.passwordMatchValidator});
  }

  cancel() {
    this.router.navigate(['/home']);
  }

  passwordMatchValidator(formGroup: FormGroup) {
    return formGroup.get('password').value === formGroup.get('confirmPassword').value ? null : {mismatch: true};
  }
}
