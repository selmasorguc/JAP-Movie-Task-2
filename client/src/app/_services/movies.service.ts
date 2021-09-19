import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Movie } from '../_models/movie';
import { Rating } from '../_models/rating';


@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  baseUrl: string = "https://localhost:5001/";
  movies: any;

  constructor(private http: HttpClient, private router: RouterModule) { }

  getMovies() {
    return this.http.get<Movie[]>(this.baseUrl + "movies");
  }

  getPagedMovies(page: number, itemsPerPage: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<Movie[]>(this.baseUrl + "movies/paged", { params: params });
  }

  getTVShows() {
    return this.http.get<Movie[]>(this.baseUrl + "tvshows");
  }

  getPagedTVShows(page: number, itemsPerPage: number) {
    let params = new HttpParams();
    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<Movie[]>(this.baseUrl + "movies/tvshows/paged", { params: params });
  }

  addRating(rating: Rating) {
    return this.http.post<number>(this.baseUrl + "ratings/add", rating);
  }
  
}
