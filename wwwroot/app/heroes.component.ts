import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable }        from 'rxjs/Observable';

import { Client, Hero } from './hero.client';
import { SignalRService }       from './signalr.service';

@Component({
    selector: 'my-heroes',
    templateUrl: 'app/heroes.component.html',
    styleUrls: ['app/heroes.component.css']
})
export class HeroesComponent implements OnInit {
    selectedHero: Hero;
    heroes: Hero[];

    constructor(
        private router: Router,
        private client: Client,
        private signalRService: SignalRService) {
        }

    onSelect(hero: Hero): void {
        this.selectedHero = hero;
    }

    getHeroes(): void {
        this.client.apiHeroesGet().toPromise().then(heroes => this.heroes = heroes);
    }

    ngOnInit(): void {
        this.getHeroes();
        this.signalRService.messageReceived.subscribe((data: any) => {
            this.getHeroes();
        });
    }

    gotoDetail(): void {
        this.router.navigate(['/detail', this.selectedHero.id]);
    }

    add(name: string): void {
        name = name.trim();
        if (!name) { return; }

        let newHero = new Hero({ id: 0, name: name });

        this.client.apiHeroesPost(newHero)
            .toPromise()
            .then(hero => {
                this.heroes.push(hero);
                this.selectedHero = null;
                this.signalRService.send('add');
            });
    }

    delete(hero: Hero): void {
        this.client.apiHeroesByIdDelete(hero.id)
            .toPromise()
            .then(() => {
                this.heroes = this.heroes.filter(h => h !== hero);
                if (this.selectedHero === hero)
                {
                    this.selectedHero = null;
                }

                this.signalRService.send('delete');
            });
    }

}
