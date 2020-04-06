/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PatronService } from './patron.service';

describe('Service: Patron', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PatronService]
    });
  });

  it('should ...', inject([PatronService], (service: PatronService) => {
    expect(service).toBeTruthy();
  }));
});
