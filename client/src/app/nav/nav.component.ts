import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  isLoggedIn = false;
  user: User;

  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
   
  }

  login() {
    this.userService.login(this.model).subscribe(response => {
      this.isLoggedIn = true;
      this.user = JSON.parse(this.userService.getCurrentUser());
      this.toastr.success("Hello there, " + this.user.username + "!");
    })
  }

}
