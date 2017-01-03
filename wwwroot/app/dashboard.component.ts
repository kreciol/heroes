import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable }        from 'rxjs/Observable';

import { Client, Hero } from './hero.client';

@Component({
    selector: 'my-dashboard',
    templateUrl: 'app/dashboard.component.html',
    styleUrls: ['app/dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    heroes: Hero[] = [];

    constructor(private router: Router, private client: Client) { }

    ngOnInit(): void {
        this.client.apiHeroesGet()
        .toPromise()
        .then(heroes => this.heroes = heroes.slice(1, 5));
    }

    gotoDetail(hero: Hero) {
        let link = ['/detail', hero.id];
        this.router.navigate(link);
     }
}
