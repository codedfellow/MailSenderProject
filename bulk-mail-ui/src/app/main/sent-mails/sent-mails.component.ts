import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-sent-mails',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './sent-mails.component.html',
  styleUrl: './sent-mails.component.scss'
})
export class SentMailsComponent {
}
