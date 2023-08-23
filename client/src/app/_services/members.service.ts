import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(username: string)
  {
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  /* before using jwt interceptor used this way to send auth header
  getMember(username: string)
  {
    return this.http.get<Member>(this.baseUrl + 'users/' + username, this.getHttpOptions())
  }

  getHttpOptions(){
    const userString = localStorage.getItem('user');
    if(!userString) return;

    const user = JSON.parse(userString);

    return {
      headers: new HttpHeaders({
          Authorization: 'Bearer ' + user.token
      })
    }
  }*/
}
