import { Injectable } from '@angular/core';
import { Photo } from '../../photos/photo'
import { Lightbox, LightboxConfig, LightboxEvent, LIGHTBOX_EVENT, IEvent, IAlbum } from 'angular2-lightbox';
import { Subscription } from 'rxjs/Subscription';


@Injectable()
export class LightboxService {

  public photos: Array<Photo>;
  private _options: Object;
  private _subscription: Subscription;
  constructor(
    private _lightbox: Lightbox,
    private _lightboxEvent: LightboxEvent,
    private _lighboxConfig: LightboxConfig
  ) { 
    this.photos = [];
    this._options = {};
    _lighboxConfig.fadeDuration = 1;
  }

  setAlbum(photos: Array<Photo>) {
    this.photos = photos;
  }
  open(index: number): void {
    this._subscription = this._lightboxEvent.lightboxEvent$.subscribe((event: IEvent) => this._onReceivedEvent(event));

    // override the default config
    this._lightbox.open(this.photos, index, { wrapAround: true, showImageNumberLabel: true });
  }

  private _onReceivedEvent(event: IEvent): void {
    if (event.id === LIGHTBOX_EVENT.CLOSE) {
      this._subscription.unsubscribe();
    }
  }
}
