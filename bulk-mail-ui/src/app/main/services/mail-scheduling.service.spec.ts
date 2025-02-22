import { TestBed } from '@angular/core/testing';

import { MailSchedulingService } from './mail-scheduling.service';

describe('MailSchedulingService', () => {
  let service: MailSchedulingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MailSchedulingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
