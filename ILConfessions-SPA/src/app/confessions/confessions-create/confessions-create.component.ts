import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Confession } from 'src/app/_models/confession';
import { ConfessionService } from 'src/app/_services/confession.service';

@Component({
  selector: 'app-confessions-create',
  templateUrl: './confessions-create.component.html',
  styleUrls: ['./confessions-create.component.css']
})
export class ConfessionsCreateComponent implements OnInit {
  confession: Confession;
  createConfessionForm: FormGroup;

  constructor(private confessionService: ConfessionService, private alertify: AlertifyService,
              private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.createFormF();
  }

  createConfession() {
    if (this.createConfessionForm.valid) {
      this.confession = Object.assign({}, this.createConfessionForm.value);
    }
    this.confessionService.createConfession(this.confession).subscribe(() => {
      this.alertify.success('Confession created successfully');
    }, err => {
      this.alertify.error(err);
    }, () => {
        this.router.navigate(['/confessions']);
    });
  }

  createFormF() {
    this.createConfessionForm = this.formBuilder.group({
      city: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  cancel() {
    this.router.navigate(['/confessions']);
  }
}
