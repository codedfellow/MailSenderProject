import { Routes } from '@angular/router';
import { LayoutComponent } from './main/layout/layout.component';
import { canActivateAuthPages } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./main/main.module').then(m => m.MainModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
    canActivate: [canActivateAuthPages]
  },
];
