import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { PhotosComponent } from './photos/photos.component';
import { RsvpComponent } from './rsvp/rsvp.component';


const routes: Routes = [
  { path: "about", component: AboutComponent },
  { path: "photos", component: PhotosComponent},
  { path: "rsvp", component: RsvpComponent},
  { path: "**", component: HomeComponent }
]

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
