import { Component, EventEmitter, Output } from '@angular/core';

import { AccessibilityHelp, Alignment, Autoformat, AutoImage, Autosave, Base64UploadAdapter, BlockQuote, BlockToolbar, Bold, CKFinder, CKFinderUploadAdapter, ClassicEditor, CloudServices, Code, CodeBlock, DataFilter, DataSchema, EasyImage, EditorConfig, Essentials, FindAndReplace, FontBackgroundColor, FontColor, FontFamily, FontSize, FullPage, GeneralHtmlSupport, Heading, Highlight, HorizontalLine, HtmlComment, HtmlEmbed, Image, ImageBlock, ImageCaption, ImageInline, ImageInsert, ImageInsertViaUrl, ImageResize, ImageStyle, ImageToolbar, ImageUpload, Indent, IndentBlock, Italic, LegacyList, LegacyListProperties, LegacyTodoList, Link, LinkImage, List, ListProperties, Markdown, MediaEmbed, MediaEmbedToolbar, Mention, Minimap, PageBreak, Paragraph, PasteFromOffice, RemoveFormat, RestrictedEditingMode, SelectAll, ShowBlocks, SimpleUploadAdapter, SourceEditing, SpecialCharacters, SpecialCharactersArrows, SpecialCharactersCurrency, SpecialCharactersEssentials, SpecialCharactersLatin, SpecialCharactersMathematical, SpecialCharactersText, StandardEditingMode, Strikethrough, Style, Subscript, Superscript, Table, TableCaption, TableCellProperties, TableColumnResize, TableProperties, TableToolbar, Template, TextPartLanguage, TextTransformation, Title, TodoList, Underline, Undo, WordCount } from 'ckeditor5';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

@Component({
  selector: 'app-custom-ck-editor',
  templateUrl: './custom-ck-editor.component.html',
  styleUrl: './custom-ck-editor.component.scss'
})
export class CustomCkEditorComponent {
  public model = {
    editorData: ''
  };

  @Output() ckeditorEventEmitter: EventEmitter<any> = new EventEmitter<any>()
  @Output() ckeditorDataEmitter:  EventEmitter<string> = new EventEmitter<string>()

  public config: EditorConfig = {
    toolbar: ['undo', 'redo', '|', 'bold', 'italic', 'numberedList', 'bulletedList', 'GeneralHtmlSupport', 'Code', 'Strikethrough', 'Subscript', 'Superscript', 'Underline', 'Alignment', 'BlockQuote', 'CodeBlock', 'FindAndReplace', 'FontBackgroundColor', 'FontColor', 'FontSize', 'FontFamily', 'Heading', 'Highlight', 'HorizontalLine', 'HtmlEmbed', 'DataFilter', 'DataSchema', 'HtmlComment', 'FullPage', 'AutoImage', 'ImageBlock', 'ImageInline', 'Image', 'ImageCaption', 'ImageResize', 'ImageStyle', 'ImageToolbar', 'ImageUpload', 'ImageInsert', 'ImageInsertViaUrl','Indent','IndentBlock','TextPartLanguage','Link','LinkImage','TodoList','MediaEmbed','MediaEmbedToolbar','Mention','PageBreak','PasteFromOffice','RemoveFormat','SelectAll','ShowBlocks','SourceEditing','SpecialCharacters','SpecialCharactersEssentials','SpecialCharactersArrows','SpecialCharactersCurrency','SpecialCharactersLatin','SpecialCharactersMathematical','SpecialCharactersText','Style','Table','TableCellProperties','TableProperties','TableToolbar','TableCaption','TableColumnResize','TextTransformation','BlockToolbar','AccessibilityHelp','WordCount'],
    image: {
      toolbar: ['toggleImageCaption', 'imageTextAlternative', 'ckboxImageEdit','mediaEmbed', 'insertTable']
    },
    mention: {
      feeds: [
        {
          marker: '@',
          feed: ['@Barney', '@Lily', '@Marry Ann', '@Marshall', '@Robin', '@Ted'],
          minimumCharacters: 1
        }
      ]
    },
    plugins: [
      Bold,
      Essentials,
      Italic,
      Mention,
      Paragraph,
      Undo,
      GeneralHtmlSupport,
      FullPage,
      // Autoformat, 
      // Autosave, 
      Code,
      Strikethrough,
      Subscript,
      Superscript,
      Underline,
      List,
      // CKFinder, //This didn't work
      // CKFinderUploadAdapter,
      Alignment,
      BlockQuote,
      // CloudServices,
      CodeBlock,
      // EasyImage, // Did not work
      FindAndReplace,
      FontBackgroundColor,
      FontColor,
      FontSize,
      FontFamily,
      // Title,
      Heading,
      Highlight,
      HorizontalLine,
      HtmlEmbed,
      DataFilter,
      DataSchema,
      HtmlComment,
      FullPage,
      AutoImage,
      ImageBlock,
      ImageInline,
      Image,
      ImageCaption,
      ImageResize,
      ImageStyle,
      ImageToolbar,
      ImageUpload, //Makes on changes getdata to fail. TO review later
      ImageInsert, //Makes on changes getdata to fail. TO review later
      ImageInsertViaUrl,
      Indent,
      IndentBlock,
      TextPartLanguage,
      Link,
      LinkImage,
      // LegacyList, //didn't work
      // LegacyTodoList, //didn't work
      // LegacyListProperties, //didn't work
      // List, //didn't work
      TodoList,
      // ListProperties, //didn't work
      // Markdown, //didn't work
      MediaEmbed,
      MediaEmbedToolbar,
      Mention,
      // Minimap,
      PageBreak,
      PasteFromOffice,
      RemoveFormat,
      // RestrictedEditingMode,
      // StandardEditingMode,
      SelectAll,
      ShowBlocks,
      SourceEditing,
      SpecialCharacters,
      SpecialCharactersEssentials,
      SpecialCharactersArrows,
      SpecialCharactersCurrency,
      SpecialCharactersLatin,
      SpecialCharactersMathematical,
      SpecialCharactersText,
      Style,
      Table,
      TableCellProperties,
      TableProperties,
      TableToolbar,
      TableCaption,
      TableColumnResize,
      TextTransformation,
      BlockToolbar,
      AccessibilityHelp,
      Base64UploadAdapter,
      SimpleUploadAdapter,
      WordCount,
    ],
    htmlSupport: {
      allow: [
        {
          name: /.*/,
          attributes: true,
          classes: true,
          styles: true
        }
      ]
    },
    mediaEmbed: {
      // Configuration
      // ...
    },
    table: {
      contentToolbar: ['tableColumn', 'tableRow', 'mergeTableCells']
    },
    licenseKey: '<YOUR_LICENSE_KEY>',
    // mention: {
    //     Mention configuration
    // }
  }

  public Editor = ClassicEditor;

  onChange({ editor }: ChangeEvent) {
    // console.log('editor...', editor);
    this.ckeditorEventEmitter.emit(editor)
    const data = editor.getData();

    // console.log('editor data only...', data);
    this.ckeditorDataEmitter.emit(data)
  }
}
