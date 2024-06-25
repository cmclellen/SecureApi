import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface City {
  city: string;
  latitude: number;
  longitude: number;
  country: string;
  iso2: string;
  state: string;
  population: number;
  populationProper: number;
}

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http: HttpClient) { }

  getCities(): Observable<City[]> {
    return this.http.get<City[]>('http://localhost:7284/api/GetPlaces');
  }
}
