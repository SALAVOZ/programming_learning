import { TestBed } from '@angular/core/testing';

import { LkService } from './lk.service';

describe('LkService', () => {
  let service: LkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
