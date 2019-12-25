import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { LoginModalComponent } from './login-modal/login-modal.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  bsModalRef: BsModalRef;
  user: User;

  constructor(public authService: AuthService, private alertify: AlertifyService,
              private router: Router, private modalService: BsModalService) { }

  ngOnInit() {
  }

  loginModal(loginM: any) {
    const initialState = {
      loginM,
      title: 'ConfessionsIL - Login Modal'
    };
    this.bsModalRef = this.modalService.show(LoginModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.authService.decodedToken = null;
    this.alertify.message('Logged out successfully');
    this.router.navigate(['/home']);
  }
}
