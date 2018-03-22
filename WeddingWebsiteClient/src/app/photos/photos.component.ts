import { Component, OnInit } from '@angular/core';
import { IPhotoService } from '../services/photo/iphoto.service';
import { PhotoService } from '../services/photo/photo.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css'],
  providers: [PhotoService]
})
export class PhotosComponent implements OnInit {

  private routeSubscription: any;
  private _route: ActivatedRoute;
  private _albumTitle: string;
  private maximumPhotosPerRow: number = 3;

  approvedRoutes: string[];
  photoService: IPhotoService;
  photos : Array<string>;
  showRoutes : boolean = true;

  constructor(photoService : PhotoService, route: ActivatedRoute) { 
    this.photoService = photoService;
    this._route = route;
  }

  ngOnInit() {
    this.routeSubscription = this._route.params.subscribe(params => {
      this._albumTitle = params['albumtitle'];
      this.getAlbumFor(this._albumTitle);
    });
 
    this.photoService.getApprovedRoutes().then(result => this.approvedRoutes = result);
    
  }
  
  getPhotosInGroupsOfThree: Function = () : Array<Array<string>> => {
    let photos: Array<Array<string>> = []
    for (let i = 0; i < this.photos.length/this.maximumPhotosPerRow; i++) {
      let photoRow : Array<string> = [];
      for (let j = 0; j < this.maximumPhotosPerRow; j++) {
        var photo = this.photos[(this.maximumPhotosPerRow*i)+j];
        if (photo)
          photoRow.push(photo)
      }
      photos.push(photoRow)      
    }
    return photos;

    };

  getAlbumFor(title: string) : void {
    if (title)
      this.photoService.getAllPhotos(title)
        .then(result => {
          this.photos = result;
          this.toggleImageRouteVisibility()
        });
  }

  toggleImageRouteVisibility() : void{
    this.showRoutes = !this.showRoutes
  }
  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }

}
