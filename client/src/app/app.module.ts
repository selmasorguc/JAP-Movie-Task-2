import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HttpClientModule } from '@angular/common/http';
import { MoviesComponent } from './movies/movies.component';
import { MovieCardComponent } from './movie-card/movie-card.component';
import { TvShowsComponent } from './tv-shows/tv-shows.component';
import { SharedModule } from './_modules/shared.module';
import { MediaComponent } from './media/media.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SearchListComponent } from './search-list/search-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ServerErrorComponent } from './errors/server-error/server-error.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MoviesComponent,
    MovieCardComponent,
    TvShowsComponent,
    MediaComponent,
    SearchListComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    TabsModule.forRoot(),
    RatingModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
