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

  approvedRoutes: string[] = ["iris"]
  photoService: IPhotoService;
  photos : Array<string>;

  constructor(photoService : PhotoService, route: ActivatedRoute) { 
    this.photoService = photoService;
    this._route = route;
  }

  ngOnInit() {
    this.routeSubscription = this._route.params.subscribe(params => {
      this._albumTitle = params['albumtitle'];
      this.getAlbumFor(this._albumTitle);
    });
 
    
  }
  
  getAlbumFor(title: string) : void {
    if (title)
      this.photoService.GetAllPhotos(title).then(result => this.photos = result);
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }

}
