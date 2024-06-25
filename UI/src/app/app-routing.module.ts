import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFailedComponent } from './login-failed/login-failed.component';
import { CityListingComponent } from './city-listing/city-listing.component';
import { MsalGuard } from '@azure/msal-angular';

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

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}