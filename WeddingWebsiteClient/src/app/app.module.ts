import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { AlertModule } from 'ngx-bootstrap';
import { AngularFireModule } from 'angularfire2';


import { AppComponent } from './app.component';
import { WeddingLocationComponent } from './wedding-location/wedding-location.component';
import { AboutComponent } from './about/about.component';
import { AppRoutingModule, routeComponents } from './/app-routing.module';
import { HomeComponent } from './home/home.component';

import ApplicationConfiguration from '../config.json';
import { PhotoService } from './services/photo/photo.service';
import { PhotosComponent } from './photos/photos.component';
import { RsvpComponent } from './rsvp/rsvp.component';
import { RsvpService } from './services/rsvp/rsvp.service';
import { FormsModule } from '@angular/forms';
import { ConfigurationService } from './services/configuration/configuration.service';
import { HttpService } from './services/http/http.service';
import { PhotoComponent } from './shared/photo/photo.component';
import { ActivatedRoute } from '@angular/router/src/router_state';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    WeddingLocationComponent,
    HomeComponent,
    PhotosComponent,
    RsvpComponent,
    AboutComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpModule,
    AngularFireModule.initializeApp(ApplicationConfiguration.firebaseConfig),
    AlertModule.forRoot(),
    FormsModule
    
  ],
  providers: [
    PhotoService,
    RsvpService,
    ConfigurationService,
    HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
 }
