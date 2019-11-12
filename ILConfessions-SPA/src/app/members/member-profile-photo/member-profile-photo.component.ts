import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-member-profile-photo',
  templateUrl: './member-profile-photo.component.html',
  styleUrls: ['./member-profile-photo.component.css']
})
export class MemberProfilePhotoComponent implements OnInit {
  @Input() photo: Photo[];

  constructor() { }

  ngOnInit() {
  }

}
