import { TestBed } from '@angular/core/testing';

import { GenreMenuService } from './genre-menu.service';

describe('GenreMenuService', () => {
  let service: GenreMenuService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenreMenuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
