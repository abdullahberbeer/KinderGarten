import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './_models/user';
import { AuthService } from './_services/auth.service';
import { UserService } from './_services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  user!:User;
  jwtHelper=new JwtHelperService();
  title = 'ClientApp';
constructor(private authService:AuthService,private userService:UserService){}

ngOnInit(){
  const token = localStorage.getItem("token");
  if(token){
    this.authService.decodedToken=this.jwtHelper.decodeToken(token);



  }
}

}

