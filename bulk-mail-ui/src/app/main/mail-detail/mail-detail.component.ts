import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MailService } from '../services/mail.service';
import { MailLogModel } from '../models/mail-log.model';
import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mail-detail',
  imports: [CommonModule, SharedModule],
  templateUrl: './mail-detail.component.html',
  styleUrl: './mail-detail.component.scss'
})
export class MailDetailComponent implements OnInit, OnDestroy {

  route = inject(ActivatedRoute)
  mailService = inject(MailService)

  subscriptions: Subscription = new Subscription();
  activeMailId: string = ''
  fetchMailDetail: boolean = false;
  mailDetail?: MailLogModel

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe()
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.activeMailId = params.get('mailId') ?? ''
        console.log('mailId...',this.activeMailId)
        if (this.activeMailId) {
          this.getMailDetail(this.activeMailId)
        }
      }
    })
  }

  getMailDetail(mailId: string){
    this.fetchMailDetail = true
    this.mailService.getSingleMailDetail(mailId).subscribe({
      next: (res) => {
        this.mailDetail = res
        if (this.mailDetail) {
          this.mailDetail.recipientsArray = this.mailDetail.recipients.split(';')
        }
        console.log('mail detail...',this.mailDetail)
        this.fetchMailDetail = false
      },
      error: (err) => {
        console.error('mail detail err...',err)
        this.fetchMailDetail = false
      }
    })
  }
}
