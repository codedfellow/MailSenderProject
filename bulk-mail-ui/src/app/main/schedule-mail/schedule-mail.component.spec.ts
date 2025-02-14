import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleMailComponent } from './schedule-mail.component';

describe('ScheduleMailComponent', () => {
  let component: ScheduleMailComponent;
  let fixture: ComponentFixture<ScheduleMailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleMailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleMailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
