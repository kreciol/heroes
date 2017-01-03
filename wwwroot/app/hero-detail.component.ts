import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable }        from 'rxjs/Observable';

import { Client, Hero } from './hero.client';

@Component({
  selector: 'my-hero-detail',
  templateUrl: 'app/hero-detail.component.html',
  styleUrls: ['app/hero-detail.component.css']
})
export class HeroDetailComponent implements OnInit {
  @Input()
  hero: Hero;

  constructor(
    private client: Client,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      let id = +params['id'];
      this.client.apiHeroesByIdGet(id).toPromise().then(hero => this.hero = hero);
    });
  }

  goBack(): void {
    window.history.back();
  }

  save(): void {
    this.client.apiHeroesByIdPut(this.hero.id, this.hero)
      .toPromise()
      .then(this.goBack);
  }
}
