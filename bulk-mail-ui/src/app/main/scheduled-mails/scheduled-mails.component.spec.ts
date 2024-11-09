import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduledMailsComponent } from './scheduled-mails.component';

describe('ScheduledMailsComponent', () => {
  let component: ScheduledMailsComponent;
  let fixture: ComponentFixture<ScheduledMailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduledMailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduledMailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
