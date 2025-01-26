import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { MailService } from '../services/mail.service';
import { MailLogModel } from '../models/mail-log.model';

@Component({
    selector: 'app-sent-mails',
    imports: [CommonModule, SharedModule],
    templateUrl: './sent-mails.component.html',
    styleUrl: './sent-mails.component.scss'
})
export class SentMailsComponent implements OnInit, OnDestroy {
    mailService = inject(MailService);

    displayMailsLoading: boolean = false
    mailLogs: MailLogModel[] = []

    ngOnDestroy(): void {
        // throw new Error('Method not implemented.');
    }
    ngOnInit(): void {
        // throw new Error('Method not implemented.');
        this.getSentMails()
    }

    getSentMails(){
        this.displayMailsLoading = true
        this.mailService.getSentMails().subscribe({
            next: (res) => {
                this.displayMailsLoading = false
                this.mailLogs = res
                console.log('mail logs...',this.mailLogs)
            },
            error: (err) => {
                this.displayMailsLoading = false
            }
        })
    }
}
