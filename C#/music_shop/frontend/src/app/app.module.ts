import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { RecordPlayerComponent } from './components/record-player/record-player.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { CatalogComponent } from './components/catalog/catalog.component';
import { CartComponent } from './components/cart/cart.component';
import { SongsAndAlbumsComponent } from './components/songs-and-albums/songs-and-albums.component';
import { CurrentPlaylistComponent } from './components/current-playlist/current-playlist.component';
import { AlbumPageComponent } from './components/album-page/album-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FilterPipe } from './components/pipes/filter.pipe';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { ShowComponent } from './components/cart/show/show.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { TrackComponent } from './components/track/track.component';
import {MatCardModule} from "@angular/material/card";
import { LkComponent } from './components/lk/lk.component';
import { PersonalInformationComponent } from './components/lk/personal-information/personal-information.component';
import { SettingsComponent } from './components/lk/settings/settings.component';
import { PurchaseHistoryComponent } from './components/lk/purchase-history/purchase-history.component';
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import {MatMenuModule} from "@angular/material/menu";
import { GenreMenuComponent } from './components/genre-menu/genre-menu.component';
import { AuthorPageComponent } from './components/author-page/author-page.component';
import { AboutComponent } from './components/about/about.component';
import {AUTH_IP_URL, SHOP_IP_URL} from "./app-injection-tokens";
import {environment} from "../environments/environment";
import {JwtModule} from "@auth0/angular-jwt";
import {ACCESS_TOKEN_KEY} from "./services/auth.service";
import { LoginComponent } from './components/login/login.component';
import {HttpClientModule} from "@angular/common/http";
import {AuthGuard} from "./guards/auth.guard";
import { RegisterComponent } from './components/register/register.component';
import { ForgotComponent } from './components/forgot/forgot.component';
import { AdminComponent } from './components/admin/admin.component';
import { UserComponent } from './components/admin/user/user.component';
import { SellerComponent } from './components/seller/seller.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
    declarations: [
        AppComponent,
        RecordPlayerComponent,
        HeaderComponent,
        FooterComponent,
        CatalogComponent,
        CartComponent,
        FooterComponent,
        SongsAndAlbumsComponent,
        CurrentPlaylistComponent,
        AlbumPageComponent,
        HomePageComponent,
        FilterPipe,
        ShowComponent,
        TrackComponent,
        LkComponent,
        PersonalInformationComponent,
        SettingsComponent,
        PurchaseHistoryComponent,
        GenreMenuComponent,
        AuthorPageComponent,
        AboutComponent,
        LoginComponent,
        RegisterComponent,
        ForgotComponent,
        AdminComponent,
        UserComponent,
        SellerComponent,
    ],
    entryComponents: [
      SettingsComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: LoginComponent},
            {path: 'register', component: RegisterComponent },
            {path: 'forgot', component: ForgotComponent},
            {path: 'home', component: HomePageComponent,  canActivate: [AuthGuard] },
            {path: 'songs-and-albums', component: SongsAndAlbumsComponent, canActivate: [AuthGuard]},
            {path: 'playlist', component: CurrentPlaylistComponent, canActivate: [AuthGuard]},
            {path: 'catalog', component: CatalogComponent,  canActivate: [AuthGuard] },
            {path: 'cart', component: CartComponent, canActivate: [AuthGuard]},
            {path: 'album-page', component: AlbumPageComponent, canActivate: [AuthGuard]},
            {path: 'lk', component: LkComponent, canActivate: [AuthGuard]},
            {path: 'lk/settings', component: LkComponent, canActivate: [AuthGuard]},
            {path: 'lk/purchase-history', component: LkComponent, canActivate: [AuthGuard]},
            {path: 'author-page', component: AuthorPageComponent, canActivate: [AuthGuard]},
            {path: 'about', component: AboutComponent, canActivate: [AuthGuard]},
            {path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
            {path: 'seller', component: SellerComponent, canActivate: [AuthGuard]}
        ]),
        BrowserAnimationsModule,
        MatInputModule,
        MatButtonModule,
        MatCardModule,
        MatSlideToggleModule,
        MatMenuModule,
        ReactiveFormsModule,
        JwtModule.forRoot({
        config: {
          tokenGetter,
          allowedDomains: ['localhost:44331']
        }
        }),
    ],
  providers: [
    {
      provide: AUTH_IP_URL,
      useValue: environment.authApi
    },
    {
      provide: SHOP_IP_URL,
      useValue: environment.storeApi
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
