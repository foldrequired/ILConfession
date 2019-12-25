import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-profile',
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css']
})
export class MemberProfileComponent implements OnInit {
  user: User;
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private userService: UserService,
              private authService: AuthService, private toastrService: ToastrService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.id, this.user).subscribe(next => {
      this.toastrService.success('Profile has been updated successfully');
      this.editForm.reset(this.user);
    }, err => {
      this.toastrService.error(err);
    });
  }

}
