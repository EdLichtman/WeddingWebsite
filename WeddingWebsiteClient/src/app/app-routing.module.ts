import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { PhotosComponent } from './photos/photos.component';
import { RsvpComponent } from './rsvp/rsvp.component';
import { getRouteComponents } from './app-routing.util';


const routes: Routes = [
  { path: "about", component: AboutComponent},
  { path: "rsvp", component: RsvpComponent},
  { path: "photos", component: PhotosComponent},
  { path: "photos/:albumtitle", component: PhotosComponent},
  { path: "**", component: HomeComponent }
]

export const routeComponents = getRouteComponents(routes);
@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
