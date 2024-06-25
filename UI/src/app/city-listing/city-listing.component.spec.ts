import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityListingComponent } from './city-listing.component';

describe('CityListingComponent', () => {
  let component: CityListingComponent;
  let fixture: ComponentFixture<CityListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityListingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
