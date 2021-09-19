import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Movie } from '../_models/movie';
import { SearchService } from '../_services/search.service';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css']
})
export class SearchListComponent implements OnInit {
  @Output() searchActive = new EventEmitter<boolean>();
  searchFrom: FormGroup = new FormGroup({
    searchQuery: new FormControl()
  });
  media: Movie[] = [];

  constructor(private searchService: SearchService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  searchMedia() {
    let searchQuery = this.searchFrom.value.searchQuery;
    if (searchQuery !== '' && searchQuery.length >= 2) {
      this.searchService.searchMedia(searchQuery).subscribe((response) => {
        this.media = response;
        this.searchActive.emit(true);
      });
    }

    if(searchQuery === '') {
      this.media = [];
      this.searchActive.emit(false);
    }
  }
}
