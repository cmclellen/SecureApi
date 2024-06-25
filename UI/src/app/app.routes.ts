import { Routes } from '@angular/router';
import { CityListingComponent } from './city-listing/city-listing.component';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { 
        path: '', 
        canActivateChild: [MsalGuard],
        component: CityListingComponent 
    },
];
