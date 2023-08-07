import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser(); //used if we close browser without logout therfore persit login
  }

  /*getCurrentUser()
  {
    this.accountService.currentUser$.subscribe({
        next: user => this.loggedIn = !!user, //double exclamation turns variable to bool
        error: error => console.log(error)
    })
  }*/
  
  login()
  {
    this.accountService.login(this.model).subscribe({
        next: response => {
        console.log(response);
      },
        error: error => console.log(error)
    });
    console.log(this.model);
  }

  logout()
  {
    this.accountService.logout();
  }
}
