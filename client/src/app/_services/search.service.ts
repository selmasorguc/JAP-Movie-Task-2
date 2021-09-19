import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../_models/movie';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  baseUrl: string = "https://localhost:5001/";

  constructor(private http: HttpClient) { }

  searchMedia(query: string) {
    let params = new HttpParams().set("query", query);
    return this.http.get<Movie[]>(this.baseUrl + "movies/search", { params: params });
  }

}
