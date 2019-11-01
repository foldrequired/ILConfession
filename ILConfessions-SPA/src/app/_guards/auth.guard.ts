import { Injectable } from '@angular/core';
import { CanActivate, Router, CanDeactivate } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('Only valid users are allowed to visit that page');
    this.router.navigate(['/']);
    return false;
  }
}
