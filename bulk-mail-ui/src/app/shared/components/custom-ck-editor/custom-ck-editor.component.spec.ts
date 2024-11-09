import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomCkEditorComponent } from './custom-ck-editor.component';

describe('CustomCkEditorComponent', () => {
  let component: CustomCkEditorComponent;
  let fixture: ComponentFixture<CustomCkEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomCkEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomCkEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
