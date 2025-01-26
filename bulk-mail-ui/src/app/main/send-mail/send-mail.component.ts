import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MailRecipientsModel } from '../models/mail-recipients.model';
import { MailService } from '../services/mail.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-send-mail',
    imports: [CommonModule, SharedModule],
    templateUrl: './send-mail.component.html',
    styleUrl: './send-mail.component.scss'
})
export class SendMailComponent {
  mailService = inject(MailService);
  router = inject(Router);

  mailRecipients: MailRecipientsModel[] = [];
  displaySendMailLoading: boolean = false

  mailBody: string = "";

  fb = inject(FormBuilder);

  mailForm: FormGroup = this.fb.group({
    mailSubject: ['',[Validators.required]],
  })

  mailReceiptForm: FormGroup = this.fb.group({
    // mailRecipient: ['',[Validators.required,Validators.email]]
    mailRecipient: ['',[Validators.email]]
  })  

  onEditorDataChange(editorData: string){
    // console.log('ckeditor data...',editorData)
    this.mailBody = editorData
  }

  onEditorChangeEvent(editorData: any){
    // console.log('ckeditor event...',editorData)
  }

  addMailRecipient(){

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

  sendMail(){
    let mailFormVal = this.mailForm.getRawValue();

    let body: any = {
      mailBody: this.mailBody,
      mailSubject: mailFormVal.mailSubject,
      receiversList: this.mailRecipients
    }

    this.displaySendMailLoading = true
    this.mailService.sendMail(body).subscribe({
      next: (res) => {
        Swal.fire('',res.message,'success')
        this.displaySendMailLoading = false
        this.router.navigate([''])
      },
      error: (err) => {
        this.displaySendMailLoading = false
      }
    })
  }

  removeRecipient(mailIndex: number, event: any){
    console.log('double clicked mail...',mailIndex,event)
    event.preventDefault();

    this.mailRecipients.splice(mailIndex);
  }
}
