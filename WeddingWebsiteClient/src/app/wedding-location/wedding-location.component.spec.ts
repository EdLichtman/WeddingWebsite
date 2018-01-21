import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WeddingLocationComponent } from './wedding-location.component';

describe('WeddingLocationComponent', () => {
  let component: WeddingLocationComponent;
  let fixture: ComponentFixture<WeddingLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeddingLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeddingLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
