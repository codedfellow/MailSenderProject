import { Injectable } from '@angular/core';
import { AppConfig } from '../../configurations/app-config';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(private http: HttpClient) {}

  sendMail(body: any) {

    return this.http.post<any>(`${AppConfig.ApiUrl}api/Mail/send-mail`, body, { withCredentials: true }).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }
}
