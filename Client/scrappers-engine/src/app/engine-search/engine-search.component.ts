import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchresultsService } from '../services/searchresults.service';
import { map } from 'rxjs/operators';
import { IQueryResult } from '../models/IQueryResult';

@Component({
  selector: 'app-engine-search',
  templateUrl: './engine-search.component.html',
  styleUrls: ['./engine-search.component.css']
})
export class EngineSearchComponent implements OnInit {
  term: string ="";
  isDirty =false;
  results$: Observable<IQueryResult[]>;

  constructor(private serv : SearchresultsService){}

  ngOnInit() {
	}

  getResult(){
    this.isDirty =true
    this.results$= this.serv.getSearchResponse(this.term).pipe(map(x=>x.data));
  }

}
