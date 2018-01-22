import { Injectable } from '@angular/core';
import { ConfigurationService } from '../configuration/configuration.service';
import { Http } from '@angular/http';

@Injectable()
export class HttpService {
  private _baseUri: string;
  private _http: Http;

  public setBaseUri(uri: string) {
    if (uri.charAt(uri.length - 1) === '/')
      uri = uri.substr(0, uri.length - 1);

    this._baseUri = uri;

  }
  constructor(configuration: ConfigurationService, http: Http) { 
    this.setBaseUri(configuration.apiBaseUri)
    this._http = http;
  }

  public async get(apiPath: string) : Promise<string> {
    return this._http.get(this.getApiUri(apiPath))
      .map(response => JSON.stringify(response.json()))
      .toPromise()
  }

  public async post(apiPath: string, body: any) : Promise<string> {
    return this._http.post(this.getApiUri(apiPath), body)
      .map(response => JSON.stringify(response.json()))
      .toPromise()

  }

  public appendUriSegment(firstSegment: string, secondSegment: string) : string{
    if (!secondSegment.startsWith('/'))
      secondSegment = '/' + secondSegment;
    return firstSegment + secondSegment;
  }
        
  private getApiUri(apiEndpoint: string) {
    return this.appendUriSegment(this._baseUri, apiEndpoint)
  }
}
