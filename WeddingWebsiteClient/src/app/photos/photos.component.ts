import { Component, OnInit } from '@angular/core';
import { IPhotoService } from '../services/photo/iphoto.service';
import { PhotoComponent } from '../shared/photo/photo.component';
import { PhotoService } from '../services/photo/photo.service';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css'],
  providers: [PhotoService]
})
export class PhotosComponent implements OnInit {

  photoService: IPhotoService
  photos : Array<string>;

  constructor(photoService : PhotoService) { 
    this.photoService = photoService;
  }

  ngOnInit() {
    this.photoService.GetAllPhotos("Iris").then(result => this.photos = result);
  }

}
