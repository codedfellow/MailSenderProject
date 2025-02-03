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

    return this.http.post<any>(`${AppConfig.ApiUrl}api/Mail/send-mail`, body).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }

  getSentMails() {
    return this.http.get<any>(`${AppConfig.ApiUrl}api/Mail/get-email-log`).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }

  getSingleMailDetail(mailId: string) {
    return this.http.get<any>(`${AppConfig.ApiUrl}api/Mail/get-single-mail-log/${mailId}`).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }
}
