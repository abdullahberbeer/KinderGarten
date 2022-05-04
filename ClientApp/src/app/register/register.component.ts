import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model:any={}
  constructor(private authService:AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit(): void {
    this.model.role='Student';
    this.model.gender='Erkek';
  }
register(){
this.authService.register(this.model).subscribe((success)=>{
  this.alertify.success("Kayıt başarılı.");

},err=>{
  this.alertify.error("Kayıt olurken sorun oluştu.");
},()=>{
  this.authService.login(this.model).subscribe(()=>{
    this.alertify.success("Giriş başarılı.");
  this.router.navigate(['/students'])
  })
})
}
}
