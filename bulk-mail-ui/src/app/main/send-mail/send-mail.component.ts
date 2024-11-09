import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-send-mail',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './send-mail.component.html',
  styleUrl: './send-mail.component.scss'
})
export class SendMailComponent {

  mailBody: string = "";

  fb = inject(FormBuilder);

  mailForm: FormGroup = this.fb.group({
    mailSubject: ['',[Validators.required]],
  })

  onEditorDataChange(editorData: string){
    // console.log('ckeditor data...',editorData)
    this.mailBody = editorData
  }

  onEditorChangeEvent(editorData: any){
    // console.log('ckeditor event...',editorData)
  }
}
