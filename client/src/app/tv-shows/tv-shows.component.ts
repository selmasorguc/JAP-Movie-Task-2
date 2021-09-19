import { Component, Input, OnInit } from '@angular/core';
import { Movie } from '../_models/movie';
import { MoviesService } from '../_services/movies.service';

@Component({
  selector: 'app-tv-shows',
  templateUrl: './tv-shows.component.html',
  styleUrls: ['./tv-shows.component.css']
})
export class TvShowsComponent implements OnInit {
  tvshows: Movie[] = [];
 

  constructor(private moviesService: MoviesService) { }

  ngOnInit(): void {
    this.moviesService.getTVShows().subscribe((response) => {
      this.tvshows = response;
    }, error => { console.log(error); });
  }

}
