import { Injectable } from '@angular/core';
import { IPhotoService } from './iphoto.service'
import { PhotoComponent } from '../../shared/photo/photo.component';
import { HttpService } from '../http/http.service';



@Injectable()
export class PhotoService implements IPhotoService {

  private apiEndpoint: string = "Photos";
  private _http: HttpService;
  

  constructor(private http: HttpService) { 
    this._http = http;
  }


  async GetAllPhotos(subFolder: string): Promise<string[]> {
    var photoEndpoint = this._http.appendUriSegment(this.apiEndpoint, subFolder)
    
    return this._http.get(photoEndpoint).then(result => {
      if (result === null) {
        return new Array<string>();
      }
      return JSON.parse(result);
    })
  }
}

