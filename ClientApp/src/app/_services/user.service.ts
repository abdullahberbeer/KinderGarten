import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {


 baseUrl:string='https://localhost:5001'
  constructor(private http:HttpClient) { }
  getUsers():Observable<User[]>{
    return this.http.get<User[]>(this.baseUrl+"/Users");
  }
  getUser(userId:string):Observable<User>{
    return this.http.get<User>(this.baseUrl+"/Users/"+userId);
  }
}
