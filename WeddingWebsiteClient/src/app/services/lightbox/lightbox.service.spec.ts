import { TestBed, inject } from '@angular/core/testing';

import { LightboxService } from './lightbox.service';

describe('LightboxService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LightboxService]
    });
  });

  it('should be created', inject([LightboxService], (service: LightboxService) => {
    expect(service).toBeTruthy();
  }));
});
