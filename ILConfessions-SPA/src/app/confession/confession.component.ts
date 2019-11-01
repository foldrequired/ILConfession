import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-confession',
  templateUrl: './confession.component.html',
  styleUrls: ['./confession.component.css']
})
export class ConfessionComponent implements OnInit {
  baseUrl = environment.API_URL;
  confessions: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  getConfessions() {
    this.http.get(this.baseUrl + 'api/v1/login').subscribe(res => {
      this.confessions = res;
    }, err => {
      console.log(err);
    });
  }

}
