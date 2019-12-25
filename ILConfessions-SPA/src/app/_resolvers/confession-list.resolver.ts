import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Confession } from '../_models/confession';
import { ConfessionService } from '../_services/confession.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ConfessionListResolver implements Resolve<Confession[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(private confessionService: ConfessionService, private router: Router, private toastrService: ToastrService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Confession[]> {
    return this.confessionService.getConfessions(this.pageNumber, this.pageSize).pipe(
      catchError(err => {
        this.toastrService.error('Failure - try again later');
        this.router.navigate(['/']);
        return of(null);
      })
    );
  }
}
