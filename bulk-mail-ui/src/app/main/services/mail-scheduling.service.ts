import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, map } from 'rxjs';
import { AppConfig } from '../../configurations/app-config';

@Injectable({
  providedIn: 'root'
})
export class MailSchedulingService {
  http = inject(HttpClient)

  scheduleMail(body: any) {
    return this.http.post<any>(`${AppConfig.ApiUrl}api/MailScheduling/schedule-mail`, body).pipe(
      map(response => response),
      catchError(error => { throw error.error }))
  }

  getScheduledMails() {
    return this.http.get<any>(`${AppConfig.ApiUrl}api/MailScheduling/scheduled-mails`).pipe(
      map(response => {
        return response
      }),
      catchError(error => { throw error.error }))
  }
}
