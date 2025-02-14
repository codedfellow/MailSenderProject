import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

@Component({
  selector: 'app-schedule-mail',
  imports: [CommonModule,SharedModule],
  templateUrl: './schedule-mail.component.html',
  styleUrl: './schedule-mail.component.scss'
})
export class ScheduleMailComponent {

}
