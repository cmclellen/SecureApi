import { Component, OnInit } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import {MatCardModule} from '@angular/material/card';
import { City, CityService } from '../city.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-city-listing',
  standalone: true,
  imports: [CommonModule , MatTableModule, MatCardModule],
  templateUrl: './city-listing.component.html',
  styleUrl: './city-listing.component.scss'
})
export class CityListingComponent implements OnInit {

  displayedColumns: string[] = ['city', 'state', 'country'];
  cities: City[] = [];

  constructor(private cityService: CityService) {}

  ngOnInit() {
    this.cityService.getCities().subscribe((response: any) => {
      console.log('Hello ' + process.env['API_KEY'])
      this.cities = response;
    });
  }
}
