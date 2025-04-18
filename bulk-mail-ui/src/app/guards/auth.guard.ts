import { inject } from '@angular/core';
import { CanActivateChildFn, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {

  let authService = inject(AuthService);

  let router = inject(Router);
  if (!authService.isLoggedIn()) {
    router.navigate([''])
  }

  return authService.isLoggedIn();
};

export const authGuardChild: CanActivateChildFn = (route, state) => {
  return authGuard(route, state)
};

export const canActivateAuthPages: CanActivateFn = (route, state) => {
  let authService = inject(AuthService);

  let router = inject(Router);
  if (authService.isLoggedIn()) {
    router.navigate([''])
  }

  return !authService.isLoggedIn();
};