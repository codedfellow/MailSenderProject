import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialExportsModule } from './modules/material-exports/material-exports.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CustomCkEditorComponent } from './components/custom-ck-editor/custom-ck-editor.component';

@NgModule({
  declarations: [
    CustomCkEditorComponent
  ],
  imports: [
    CommonModule,
    MaterialExportsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    RouterLinkActive,
    CKEditorModule
  ],
  exports: [
    MaterialExportsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    RouterLinkActive,
    CKEditorModule,
    CustomCkEditorComponent
  ]
})
export class SharedModule { }
