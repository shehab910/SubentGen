import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse, RegisterUser, User } from '../models/user';
import { Observable } from 'rxjs';

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
}
