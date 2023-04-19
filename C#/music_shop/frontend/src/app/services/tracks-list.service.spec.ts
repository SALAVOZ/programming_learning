import { TestBed } from '@angular/core/testing';

import { TracksListService } from './tracks-list.service';

describe('TracksListService', () => {
  let service: TracksListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TracksListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
