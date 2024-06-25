import { Routes } from '@angular/router';
import { CityListingComponent } from './city-listing/city-listing.component';
import { MsalGuard } from '@azure/msal-angular';
import { LoginFailedComponent } from './login-failed/login-failed.component';

export const routes: Routes = [
    { 
        path: '', 
        canActivate: [MsalGuard],
        component: CityListingComponent 
    },
    { 
        path: 'login-failed',         
        component: LoginFailedComponent 
    },
];
