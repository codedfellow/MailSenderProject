import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from '../guards/auth.guard';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'sent-mails',
        // canActivate: [canActivateLoggedInAccess],
        loadComponent: () => import('./sent-mails/sent-mails.component').then(mod => mod.SentMailsComponent),
        canActivate: [authGuard]
      },
      {
        path: 'send-mail',
        // canActivate: [canActivateLoggedInAccess],
        loadComponent: () => import('./send-mail/send-mail.component').then(mod => mod.SendMailComponent),
        canActivate: [authGuard]
      },
      {
        path: 'mail-detail/:mailId',
        // canActivate: [canActivateLoggedInAccess],
        loadComponent: () => import('./mail-detail/mail-detail.component').then(mod => mod.MailDetailComponent),
        canActivate: [authGuard]
      }
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class MainRoutingModule { }
