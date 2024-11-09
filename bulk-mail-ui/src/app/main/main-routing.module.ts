import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';

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
        loadComponent: () => import('./sent-mails/sent-mails.component').then(mod => mod.SentMailsComponent)
      },
      {
        path: 'send-mail',
        // canActivate: [canActivateLoggedInAccess],
        loadComponent: () => import('./send-mail/send-mail.component').then(mod => mod.SendMailComponent)
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
