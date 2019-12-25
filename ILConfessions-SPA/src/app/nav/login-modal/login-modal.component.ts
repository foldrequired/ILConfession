import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  model: any = {};
  list: any[] = [];

  constructor(public bsModalRef: BsModalRef, private authService: AuthService,
              private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged In Successfully');
    }, err => {
      this.alertify.error('Failed to login, Please try again');
    }, () => {
      this.router.navigate(['/confessions']);
    });
  }

}
