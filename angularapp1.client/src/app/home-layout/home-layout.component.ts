import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-layout',
  templateUrl: './home-layout.component.html',
  styleUrl: './home-layout.component.css'
})
export class HomeLayoutComponent {

  constructor(private authService: AuthService, private router: Router) {

  }

  logoutHandler() {
    this.authService.logout();
    this.router.navigate(["auth"])
  }
}
