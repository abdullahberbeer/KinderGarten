import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';


import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  jwtHelper=new JwtHelperService();
model:any={}
user!:User;
  constructor(public authService:AuthService,private alertfiy:AlertifyService,private router:Router,private userService:UserService) { }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.model).subscribe((next)=>{
      this.alertfiy.success("Giriş Başarılı");
      this.router.navigate(['/home'])
    },error=>{
      this.alertfiy.error("Hatalı giriş");

    })
  }
  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem("token")
    localStorage.removeItem("role")
    this.alertfiy.warning("Çıkış yapıldı..")
    this.router.navigate(['/home'])
  }

}
