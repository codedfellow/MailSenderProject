import { Injectable } from '@angular/core';
import { AppConfig } from '../../configurations/app-config';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs';
import { jwtDecode } from 'jwt-decode'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
  }

  login(body: any) {

    return this.http.post<any>(`${AppConfig.ApiUrl}api/Auth/login`, body, { withCredentials: true }).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }

  register(body: any) {

    return this.http.post<any>(`${AppConfig.ApiUrl}api/Auth/register`, body, { withCredentials: true }).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('bulkmailtoken') ? true : false
  };

  decryptToken(): any {
    let token = localStorage.getItem('bulkmailtoken');
    if (token) {
      let decoded: any = jwtDecode(token);
      return decoded;
    }

    return null;
  }

  // getUserInfo() : UserModel | undefined {
  //   let token = localStorage.getItem('apextoken');
  //   if (token) {
  //     let decoded: any = jwtDecode(token);
  //     console.log('decoded token...', decoded)
  //     let user: UserModel = {
  //       // userType: decoded.UserType,
  //       userName: '',
  //       usertypeId: Number(decoded.UserTypeId)
  //     }

  //     console.log('keys...',Object.keys(decoded))
  //     let userNameKey = Object.keys(decoded).find(key => key.endsWith("name")) ?? ""
  //     console.log('user name key...',userNameKey)
  //     user.userType = user.usertypeId == UserTypeEnum.NormalUser ? "Normal User" : "Staff"
  //     user.userName = decoded[userNameKey]
  //     console.log('user info...',user)
  //     return user;
  //   }

  //   return undefined;
  // }
}
