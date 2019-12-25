import { Component, OnInit } from '@angular/core';
import { ConfessionService } from 'src/app/_services/confession.service';
import { ActivatedRoute } from '@angular/router';
import { Confession } from 'src/app/_models/confession';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-confessions-list',
  templateUrl: './confessions-list.component.html',
  styleUrls: ['./confessions-list.component.css']
})
export class ConfessionsListComponent implements OnInit {
  confessions: Confession[];
  cities: any;
  pagination: Pagination;

  constructor(private confessionService: ConfessionService, private toastrService: ToastrService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.confessions = data['confessions'].result;
      this.pagination = data['confessions'].pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadConfessions();
  }

  loadConfessions() {
    this.confessionService.getConfessions(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((next: PaginationResult<Confession[]>) => {
      this.confessions = next.result;
      this.pagination = next.pagination;
    }, err => {
      this.toastrService.error(err);
    });
  }
}
