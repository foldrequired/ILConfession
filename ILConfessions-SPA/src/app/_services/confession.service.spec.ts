/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ConfessionService } from './confession.service';

describe('Service: Confession', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfessionService]
    });
  });

  it('should ...', inject([ConfessionService], (service: ConfessionService) => {
    expect(service).toBeTruthy();
  }));
});
