import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import Swal from 'sweetalert2';
import { MailRecipientsModel } from '../models/mail-recipients.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MailService } from '../services/mail.service';
import { MailSchedulingService } from '../services/mail-scheduling.service';

@Component({
  selector: 'app-schedule-mail',
  imports: [CommonModule, SharedModule],
  templateUrl: './schedule-mail.component.html',
  styleUrl: './schedule-mail.component.scss'
})
export class ScheduleMailComponent {
  mailSchedulingService = inject(MailSchedulingService);
  router = inject(Router);

  mailRecipients: MailRecipientsModel[] = [];
  displayScheduleMailLoading: boolean = false
  mailFrequency: any[] = [
    {id: 1, name: 'Daily'},
    {id: 2, name: 'Weekly'},
    {id: 3, name: 'Monthly'},
  ]

  mailBody: string = "";

  fb = inject(FormBuilder);

  scheduleMailForm: FormGroup = this.fb.group({
    mailSubject: ['', [Validators.required]],
    startDate: [null],
    endDate: [null],
    isContinuous: [false],
    frequency: [1]
  })

  mailReceiptForm: FormGroup = this.fb.group({
    // mailRecipient: ['',[Validators.required,Validators.email]]
    mailRecipient: ['', [Validators.email]]
  })

  onEditorDataChange(editorData: string) {
    // console.log('ckeditor data...',editorData)
    this.mailBody = editorData
  }

  onEditorChangeEvent(editorData: any) {
    // console.log('ckeditor event...',editorData)
  }

  addMailRecipient() {

    let formVal = this.mailReceiptForm.getRawValue();

    let newRecipient: MailRecipientsModel = {
      emailAddress: formVal.mailRecipient,
      emailName: formVal.mailRecipient
    };

    if (this.mailRecipients.find(x => x.emailAddress == newRecipient.emailAddress)) {
      this.mailReceiptForm.reset()
      return
    }
    this.mailRecipients.push(newRecipient)
    this.mailReceiptForm.reset()
  }

  scheduleMail() {
    let scheduleMailFormVal = this.scheduleMailForm.getRawValue();

    let body: any = {
      body: this.mailBody,
      subject: scheduleMailFormVal.mailSubject,
      receiversList: this.mailRecipients,
      isContinuous: scheduleMailFormVal.isContinuous,
      startDateTime: scheduleMailFormVal.startDate,
      endDate: scheduleMailFormVal.endDate,
      frequency: scheduleMailFormVal.frequency
    }

    this.displayScheduleMailLoading = true
    this.mailSchedulingService.scheduleMail(body).subscribe({
      next: (res) => {
        Swal.fire('', res.message, 'success')
        this.displayScheduleMailLoading = false
        this.router.navigate([''])
      },
      error: (err) => {
        this.displayScheduleMailLoading = false
      }
    })
  }

  changeIsContinuous(){
    if (this.scheduleMailForm.value.isContinuous) {
      this.scheduleMailForm.controls['endDate'].reset();
      this.scheduleMailForm.controls['endDate'].disable()
    }
    else{
      this.scheduleMailForm.controls['endDate'].enable()
    }
  }

  removeRecipient(mailIndex: number, event: any) {
    console.log('double clicked mail...', mailIndex, event)
    event.preventDefault();

    this.mailRecipients.splice(mailIndex);
  }
}
