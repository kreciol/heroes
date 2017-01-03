import { NgModule }             from '@angular/core';
import { BrowserModule }        from '@angular/platform-browser';
import { FormsModule }          from '@angular/forms';
import { HttpModule }           from '@angular/http';
import './rxjs-extensions';

import { AppComponent }         from './app.component';
import { HeroDetailComponent }  from './hero-detail.component';
import { HeroesComponent }      from './heroes.component';

import { API_BASE_URL, Client }        from './hero.client';

import { DashboardComponent }   from './dashboard.component';
import { HeroSearchComponent }  from './hero-search.component';

import { routing }              from './app.routing';
import { SignalRService }       from './signalr.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    routing
  ],
  declarations: [
    AppComponent,
    HeroDetailComponent,
    HeroesComponent,
    DashboardComponent,
    HeroSearchComponent
  ],
  providers:
  [
     Client,
     { provide: API_BASE_URL, useValue: 'http://localhost:5000' },
     SignalRService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
