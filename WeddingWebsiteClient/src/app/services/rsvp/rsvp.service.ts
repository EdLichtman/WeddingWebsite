import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { BaseRequestOptions } from '@angular/http/src/base_request_options';
import { guestRequest } from './rsvpRequest';
import { Guest } from '../../rsvp/rsvp';
import { ConfigurationService } from '../configuration/configuration.service';

@Injectable()
export class RsvpService {

  private apiBaseUri: string;
  private apiEndpoint: string = "RSVP/";
  private rsvpApiAddress(): string {
    return this.apiBaseUri + this.apiEndpoint
  };

  constructor(private http: Http, private configuration: ConfigurationService) { 
    this.apiBaseUri = configuration.apiBaseUri;
  }

  getRsvpById(id: number){
    var requestUri = this.rsvpApiAddress() + `GetRSVPById?id=${id}`;

    return this.http.get(requestUri)
      .map(response => response.json())
  }
  async ResetUserResponseStatus(id: number): Promise<void> {
    var requestUri = this.rsvpApiAddress() + `ResetUserResponseStatus?id=${id}`

    await this.http.get(requestUri).toPromise();
  }
  async SubmitRsvp(id: number, guests: Array<Guest>): Promise<void> {
    var requestUri = this.rsvpApiAddress() + `SubmitRSVP`;
    var body = {
      Id : id,
      Guests: guests ? guests.map(guest => new guestRequest(guest)) : new Array<guestRequest>()
    };

    await this.http.post(requestUri, body).toPromise();
  }

}
