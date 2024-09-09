import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse, RegisterUser, User } from '../models/user';
import { Observable } from 'rxjs';
import { getJwtTokenPayload } from '../../utils/utils';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public register(user: RegisterUser): Observable<AuthResponse> {
    return this.http.post <AuthResponse>("/api/account/register", user)
  }

  public login(user: User): Observable<AuthResponse> {
    return this.http.post<AuthResponse>("/api/account/login", user)
  }

  public getMyName(): Observable<string> {
    return this.http.get("/api/account", { responseType: "text" })
  }

  public logout() {
    localStorage.removeItem("token");
  }

  public isLoggedIn(): boolean {
    return !!localStorage.getItem("token")
  }

  public isTokenExpired(): boolean {
    const token = localStorage.getItem("token")
    if (!token) {
      return true
    }
    const payload = getJwtTokenPayload(token);
    const isExpired = payload.exp < Date.now() / 1000
    if (isExpired) {
      localStorage.removeItem("token");
    }
    return isExpired;
  }
}
