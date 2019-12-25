import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
              private router: Router, private toastrService: ToastrService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.toastrService.success('Logged In Successfully');
    }, err => {
      this.toastrService.error('Failed to logds');
    }, () => {
      this.router.navigate(['/confessions']);
    });
  }

}
