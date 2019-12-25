import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private toastrService: ToastrService) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

    this.toastrService.error('Only valid users are allowed to visit that page');
    this.router.navigate(['/']);
    return false;
  }
}
