import { Injectable } from '@angular/core';
import { guestRequest } from './rsvpRequest';
import { Guest, Rsvp } from '../../rsvp/rsvp';
import { ConfigurationService } from '../configuration/configuration.service';
import { HttpService } from '../http/http.service';

@Injectable()
export class RsvpService {

  private _http: HttpService;
  private apiEndpoint: string = "RSVP";

  constructor(http: HttpService) { 
    this._http = http;
  }

  async getRsvpById(id: number): Promise<Rsvp>{
    var requestUri = this._http.appendUriSegment(this.apiEndpoint, `GetRSVPById?id=${id}`);

    return this._http.get(requestUri).then(result => {
      var data = JSON.parse(result);
      return new Rsvp(
        data.id, 
        data.referenceName, 
        data.numberOfGuests, 
        data.hasResponded, 
        data.guests);
      }
    );
  }
  async ResetUserResponseStatus(id: number): Promise<void> {
    var requestUri = this._http.appendUriSegment(this.apiEndpoint, `ResetUserResponseStatus?id=${id}`);

    return await this._http.get(requestUri).then(result => {
      //do nothing
    });
  }
  async SubmitRsvp(id: number, guests: Array<Guest>): Promise<void> {
    var requestUri = this._http.appendUriSegment(this.apiEndpoint,`SubmitRSVP`);
    var body = {
      Id : id,
      Guests: guests ? guests.map(guest => new guestRequest(guest)) : new Array<guestRequest>()
    };

    return await this._http.post(requestUri, body).then(result => {
      //do nothing
  });
}

}
