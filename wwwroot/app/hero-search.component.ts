import { Component, OnInit } from '@angular/core';
import { Router }            from '@angular/router';
import { Observable }        from 'rxjs/Observable';
import { Subject }           from 'rxjs/Subject';

import { Client, Hero } from './hero.client';

@Component({
    selector: 'hero-search',
    templateUrl: 'app/hero-search.component.html',
    styleUrls: ['app/hero-search.component.css']
})
export class HeroSearchComponent implements OnInit {
    heroes: Observable<Hero[]>;
    private searchTerms = new Subject<string>();
    constructor(
        private client: Client,
        private router: Router) { }

    search(term: string): void {
        this.searchTerms.next(term);
    }

    ngOnInit(): void {
        this.heroes = this.searchTerms
            .debounceTime(300)        // wait for 300ms pause in events
            .distinctUntilChanged()   // ignore if next search term is same as previous
            .switchMap(term => term   // switch to new observable each time
                // return the http search observable
                ? this.client.apiHeroesGet()
                // or the observable of empty heroes if no search term
                : Observable.of<Hero[]>([]))
            .catch(error => {
                // TODO: real error handling
                console.log(error);
                return Observable.of<Hero[]>([]);
            });
    }

    gotoDetail(hero: Hero): void {
        let link = ['/detail', hero.id];
        this.router.navigate(link);
    }
}
