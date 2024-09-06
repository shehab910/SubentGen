import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthResponse, RegisterUser, User } from './models/user';
import { AuthService } from './services/auth.service';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    
  }

  register(user: RegisterUser) {
    this.authService.register(user).subscribe();
  }

  login(user: User) {
    this.authService.login(user).subscribe((res: AuthResponse) => {
      localStorage.setItem("token", res.token)
      localStorage.setItem("username", res.userName)
    });
  }

  getMyName() {
    this.authService.getMyName().subscribe((username: string) => console.log(username))
  }

  title = 'angularapp1.client';
}
