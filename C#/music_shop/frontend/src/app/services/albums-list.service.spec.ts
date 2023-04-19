import { TestBed } from '@angular/core/testing';

import { AlbumsListService } from './albums-list.service';

describe('AlbumsListService', () => {
  let service: AlbumsListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlbumsListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
