import { Component, OnInit } from '@angular/core';
import { Confession } from 'src/app/_models/confession';
import { ConfessionService } from 'src/app/_services/confession.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confessions-details',
  templateUrl: './confessions-details.component.html',
  styleUrls: ['./confessions-details.component.css']
})
export class ConfessionsDetailsComponent implements OnInit {
  confession: Confession;
  comments: Comment;

  constructor(private confessionService: ConfessionService, private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadConfession();
  }

  loadConfession() {
    this.confessionService.getSingleConfession(+this.route.snapshot.params['id']).subscribe((confession: Confession) => {
      this.confession = confession;
    }, err => {
      this.alertify.error(err);
    });
  }

}
