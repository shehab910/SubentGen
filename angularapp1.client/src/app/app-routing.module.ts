import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './guards/auth.guard';
import { loginGuard } from './guards/login.guard';
import { NewSubnetComponent } from './new-subnet/new-subnet.component';

export const routes: Routes = [
  {
    path: "auth",
    component: LoginRegisterComponent,
    title: "Auth Page",
    pathMatch: "full",
    canActivate: [loginGuard],
  },
  {
    path: "home",
    component: HomeComponent,
    title: "My Subnets",
    pathMatch: "full",
    canActivate: [authGuard],
  },
  {
    path: "new-subnet",
    component: NewSubnetComponent,
    title: "New Subnet",
    pathMatch: "full",
    canActivate: [authGuard],
  },
  {
    path: "",
    pathMatch: "full",
    redirectTo: "home",
  },
  {
    path: "**",
    redirectTo: "home",
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
