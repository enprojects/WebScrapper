import { Component } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { IQueryResult } from './models/IQueryResult';
import { SearchresultsService } from './services/searchresults.service';
import {distinctUntilChanged, debounceTime, map} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})



export class AppComponent {
  title = 'scrappers-engine';
  term: string ="";
  results$: Observable<IQueryResult[]>;



  constructor(private serv : SearchresultsService){}

  ngOnInit() {
	}

  getResult(){
    // debugger;
    // this.results$ =  this.serv.getSearchResponse(this.term);
    // this.results$ .subscribe(x=>{debugger; return x;})


    this.results$= this.serv.getSearchResponse(this.term).pipe(map(x=>x.data));

  }

}
