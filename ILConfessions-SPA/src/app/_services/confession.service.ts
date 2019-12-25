import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Confession } from '../_models/confession';
import { Observable } from 'rxjs';
import { PaginationResult, Pagination } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConfessionService {
  baseUrl = environment.API_URL;

  constructor(private http: HttpClient) {}

  getConfessions(page?, itemsPerPage?): Observable<PaginationResult<Confession[]>> {
    const paginationResult: PaginationResult<Confession[]> = new PaginationResult<Confession[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Confession[]>(this.baseUrl + 'confessions', {observe: 'response', params})
    .pipe(
      map(res => {
        paginationResult.result = res.body;
        if (res.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(res.headers.get('Pagination'));
        }

        return paginationResult;
      })
    );
  }

  getSingleConfession(confessionId): Observable<Confession> {
    return this.http.get<Confession>(this.baseUrl + 'confessions/' + confessionId);
  }

  createConfession(confession: Confession) {
    return this.http.post(this.baseUrl + 'confessions', confession);
  }
}
