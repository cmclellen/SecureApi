import { Component, OnInit } from '@angular/core';
import { City, CityService } from '../city.service';

@Component({
  selector: 'app-city-listing',
  templateUrl: './city-listing.component.html',
  styleUrl: './city-listing.component.scss'
})
export class CityListingComponent implements OnInit {

  displayedColumns: string[] = ['city', 'state', 'country'];
  cities: City[] = [];

  constructor(private cityService: CityService) {}

  ngOnInit() {
    this.cityService.getCities().subscribe((response: any) => {
      this.cities = response;
    });
  }
}
