import { TestBed } from '@angular/core/testing';

import { BirthdaysService } from './birthdays.service';

describe('BirthdaysService', () => {
  let service: BirthdaysService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BirthdaysService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
