import { IQueryResult } from './../models/IQueryResult';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SearchresultsService {



  constructor(private http: HttpClient) {


  }

  getSearchResponse(term: string) {
    const url  =`https://localhost:44321/api/engineSerach/get-results/${term}`;
    return this.http.get<any>(url);
  }
}
