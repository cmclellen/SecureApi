import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule, provideZoneChangeDetection } from "@angular/core";

import { AppComponent } from "./app.component";

import { MsalBroadcastService, MsalGuard, MsalGuardConfiguration, MsalInterceptor, MsalInterceptorConfiguration, MsalModule, MsalRedirectComponent, MsalService } from "@azure/msal-angular";
import { BrowserCacheLocation, IPublicClientApplication, InteractionType, PublicClientApplication } from "@azure/msal-browser";
import { AppRoutingModule } from "./app-routing.module";
import { RouterOutlet, provideRouter } from "@angular/router";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { BaseUrlInterceptor } from "./base-url.interceptor";
import { CityListingComponent } from "./city-listing/city-listing.component";
import { CommonModule } from "@angular/common";
import { MatTableModule } from "@angular/material/table";
import { MatCardModule } from "@angular/material/card";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";

const isIE =
    window.navigator.userAgent.indexOf("MSIE ") > -1 ||
    window.navigator.userAgent.indexOf("Trident/") > -1;

var auth = {
    clientId: "7cfaf5c1-72a4-4fc5-9d01-3ee9e0d9e322",
    redirectUri: process.env['REDIRECT_URI'],
    postLogoutRedirectUri: process.env['REDIRECT_URI'],
    authority: `https://login.microsoftonline.com/${process.env['TENANT_ID']}`
};

const partScope = "cities.read";
const fullScope = `api://67226661-dd54-471e-a51e-36312accd09f/${partScope}`;
const scopes = ['openid', 'email', 'profile', 'offline_access', fullScope];

console.log(auth);

export function MSALInstanceFactory(): IPublicClientApplication {
    return new PublicClientApplication({
        auth: auth,
        cache: {
            cacheLocation: BrowserCacheLocation.LocalStorage,
            storeAuthStateInCookie: false
        },
    });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
    const baseApiUrl = <string>process.env['BASE_API_URL'];
    console.log(`Setting ${baseApiUrl}...`);
    protectedResourceMap.set(baseApiUrl, scopes);

    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap,
    };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
    return {
        interactionType: InteractionType.Redirect,
        authRequest: {
            scopes: scopes
        },
        loginFailedRoute: "login-failed"
    };
}

@NgModule({
    declarations: [AppComponent, CityListingComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        MsalModule.forRoot(
            MSALInstanceFactory(),
            MSALGuardConfigFactory(),
            MSALInterceptorConfigFactory()
        ),
        CommonModule, MatTableModule, MatCardModule, RouterOutlet, MatSlideToggleModule
    ],
    providers: [
        provideHttpClient(withInterceptorsFromDi()),
        { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true },
        {
            provide: "BASE_API_URL", useValue: process.env['BASE_API_URL']
        },
        MsalGuard,
        MsalBroadcastService,
        MsalService
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }