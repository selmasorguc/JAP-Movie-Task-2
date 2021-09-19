import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Movie } from '../_models/movie';
import { MoviesService } from '../_services/movies.service';
import { SearchService } from '../_services/search.service';

@Component({
  selector: 'app-media',
  templateUrl: './media.component.html',
  styleUrls: ['./media.component.css']
})

export class MediaComponent implements OnInit {
  movies: Movie[] = [];
  tvshows: Movie[] = [];
  //Pagination
  pageNumberM = 1;
  pageNumberT = 1;
  itemsPerPage = 6;
  //hiding load more buttons toggle
  loadMoviesButton: boolean = true;
  loadTVShowsButton: boolean = true;

  searchActive: boolean = false;


  constructor(private moviesService: MoviesService,
    private searchService: SearchService,
    private toastr: ToastrService) { }
  
  ngOnInit(): void {
    this.loadMovies();
    this.loadTVShows();
  }

  loadMovies() {
    this.moviesService.getPagedMovies(this.pageNumberM, this.itemsPerPage).subscribe((response) => {
      this.movies = response;
      this.pageNumberM++;
    }, error => { console.log(error); });
  }

  loadTVShows() {
    this.moviesService.getPagedTVShows(this.pageNumberT, this.itemsPerPage).subscribe((response) => {
      this.tvshows = response;
      this.pageNumberT++;
    }, error => { console.log(error); });
  }

  loadMoreMovies() {
    this.moviesService.getPagedMovies(this.pageNumberM, this.itemsPerPage).subscribe((response) => {

      if (response.length === 0) {
        this.loadMoviesButton = false;
        this.toastr.warning(
          'We dont have any more movies', '');
      }

      this.movies = this.movies.concat(response);
      this.pageNumberM++;
    }, error => { console.log(error); });
  }

  loadMoreTVShows() {
    this.moviesService.getPagedTVShows(this.pageNumberT, this.itemsPerPage).subscribe((response) => {

      if (response.length === 0) {
        this.loadTVShowsButton = false;
        this.toastr.warning(
          'We dont have any more TV Shows', '');
      }

      this.tvshows = this.tvshows.concat(response);
      this.pageNumberT++;
    }, error => { console.log(error); });
  }

  isSearching(data: any) {
    this.searchActive = data;
  }

}
