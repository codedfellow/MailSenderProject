import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

@Component({
    selector: 'app-scheduled-mails',
    imports: [CommonModule, SharedModule],
    templateUrl: './scheduled-mails.component.html',
    styleUrl: './scheduled-mails.component.scss'
})
export class ScheduledMailsComponent {

}
