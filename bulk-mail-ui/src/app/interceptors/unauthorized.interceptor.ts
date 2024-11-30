import { HttpEventType, HttpInterceptorFn, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, tap, throwError } from 'rxjs';

export const unauthorizedInterceptor: HttpInterceptorFn = (req, next) => {
  let router = inject(Router);
  
  return next(req).pipe(tap(event => {
    // if (event.type === HttpEventType.Response) {
    //   if (event.status == HttpStatusCode.Unauthorized) {
    //     localStorage.removeItem('bulkmailtoken');
    //     router.navigate([''])
    //   }
    // }
  }),catchError(err => {
    if (err.status === 401 || err.status === 403) {
      // Logout or redirect to login page
      localStorage.removeItem('bulkmailtoken');
        router.navigate([''])
      throw(err);
    }
   throw(err)
  }));
};
