import { Component, OnInit } from '@angular/core';
import { Movie } from '../_models/movie';
import { MoviesService } from '../_services/movies.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  movies: Movie[] = [];
  searchQuery: string = '';
  search: any = {};

  constructor(private moviesService: MoviesService) { }

  ngOnInit(): void {
    this.moviesService.getMovies().subscribe((response) => {
      this.movies = response;
    }, error => { console.log(error); });
  }
  searchMovies(query: string) {

  }
}
