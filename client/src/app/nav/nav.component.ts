import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  
  constructor(public accountService: AccountService, private router:Router, private toastr:ToastrService) { }

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
        next: response => this.router.navigateByUrl('/members'),
       // error: error => this.toastr.error(error.error) now handled by interceptor
    });
  }

  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
