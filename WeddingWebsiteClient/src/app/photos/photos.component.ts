import { Component, OnInit } from '@angular/core';
import { IPhotoService } from '../services/photo/iphoto.service';
import { PhotoService } from '../services/photo/photo.service';
import { ActivatedRoute } from '@angular/router';
import { Photo } from './photo'
import { LightboxService } from '../services/lightbox/lightbox.service';


@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css'],
  providers: [PhotoService]
})
export class PhotosComponent implements OnInit {

  private routeSubscription: any;
  private _route: ActivatedRoute;
  private lightboxService: LightboxService
  private _albumTitle: string;
  private maximumPhotosPerRow: number = 3;
  

  approvedRoutes: string[];
  photoService: IPhotoService;
  photos : Array<Photo>;
  showRoutes : boolean = true;

  constructor(photoService : PhotoService, route: ActivatedRoute, lightboxService: LightboxService) { 
    this.photoService = photoService;
    this._route = route;
    this.lightboxService = lightboxService;
  }

  ngOnInit() {
    this.routeSubscription = this._route.params.subscribe(params => {
      this._albumTitle = params['albumtitle'];
      this.getAlbumFor(this._albumTitle);
    });
 
    this.photoService.getApprovedRoutes().then(result => this.approvedRoutes = result);
    
  }
  
  open(index: number): void {
    this.lightboxService.open(index);
  }

  getPhotosInGroupsOfThree: Function = () : Array<Array<Photo>> => {
    let photos: Array<Array<Photo>> = []
    for (let i = 0; i < this.photos.length/this.maximumPhotosPerRow; i++) {
      let photoRow : Array<Photo> = [];
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
          this.photos = result.map(src => { return  new Photo(src)});
          this.lightboxService.setAlbum(this.photos);
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
