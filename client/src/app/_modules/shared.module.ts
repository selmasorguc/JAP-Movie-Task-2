import { RatingModule } from 'ngx-bootstrap/rating';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from "ngx-spinner";


import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RatingModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    RatingModule.forRoot(),
    NgbModule,
    ToastrModule.forRoot(),
    Ng2SearchPipeModule,
    TabsModule.forRoot(),
    BrowserAnimationsModule,
    NgxSpinnerModule
  ],
  exports: [
    CommonModule,
    TabsModule,
    RatingModule,
    FormsModule,
    ReactiveFormsModule,
    RatingModule,
    NgbModule,
    ToastrModule,
    Ng2SearchPipeModule,
    BrowserAnimationsModule,
    NgxSpinnerModule 
  ]
})
export class SharedModule { }
