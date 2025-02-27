import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { MailSchedulingService } from '../services/mail-scheduling.service';
import { ScheduledMailModel } from '../models/scheduled-mail.model';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-scheduled-mails',
    imports: [CommonModule, SharedModule],
    templateUrl: './scheduled-mails.component.html',
    styleUrl: './scheduled-mails.component.scss'
})
export class ScheduledMailsComponent implements OnInit, OnDestroy {

    mailSchedulingService = inject(MailSchedulingService);

    scheduledMails: ScheduledMailModel[] = []
    subscription: Subscription = new Subscription()

    constructor() {
    }
    ngOnInit(): void {
        this.getScheduledMails()
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe()
    }

    getScheduledMails() {
        this.subscription.add(
            this.mailSchedulingService.getScheduledMails().subscribe({
                next: (res) => {
                    this.scheduledMails = res
                },
                error: (err) => {

                }
            })
        )
    }
}
