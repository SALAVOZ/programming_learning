import { TestBed } from '@angular/core/testing';

import { AuthorPageService } from './author-page.service';

describe('AuthorPageService', () => {
  let service: AuthorPageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthorPageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
