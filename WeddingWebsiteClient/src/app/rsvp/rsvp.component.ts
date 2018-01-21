import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { RsvpService } from '../services/rsvp/rsvp.service';
import { OnChanges } from '@angular/core/src/metadata/lifecycle_hooks';
import { Observable } from 'rxjs/Observable';
import { Subscriber } from 'rxjs/Subscriber';
import { Subject, SubjectSubscriber } from 'rxjs/Subject';
import { Observer } from 'rxjs/Observer';
import { Rsvp, Guest } from './rsvp';

@Component({
  selector: 'app-rsvp',
  templateUrl: './rsvp.component.html',
  styleUrls: ['./rsvp.component.css']
})
export class RsvpComponent implements OnInit{ 

  currentRsvpId : number;
  //currentRsvpId : number;
  currentRsvp : Rsvp;
  rsvpService : RsvpService;
  guestResponseStatusMessage: SubjectSubscriber<string>;

  constructor(rsvpService: RsvpService) { 
    this.rsvpService = rsvpService;
    
  }

  ngOnInit() {
    
  }

  async setCurrentRsvp(id: number): Promise<void> {
    this.currentRsvp = undefined;
    await this.rsvpService.getRsvpById(id)
    .subscribe(data => {this.currentRsvp = new Rsvp(
        data.id, 
        data.referenceName, 
        data.numberOfGuests, 
        data.hasResponded, 
        data.guests);
      });
  }

  resetUserResponse(){
    this.rsvpService.ResetUserResponseStatus(this.currentRsvp.id).then(
      () => {
        this.setCurrentRsvp(this.currentRsvpId)
      }
    );
    
  }

  setUserResponseToNotGoing(){
    this.currentRsvp.savedGuests.forEach(guest => guest.isToBeDeleted = true);
    this.submitRsvp(this.currentRsvp.savedGuests)
  }
  submitRsvp(guestList: Guest[]) {
    this.rsvpService.SubmitRsvp(this.currentRsvp.id, guestList).then(
      () => this.setCurrentRsvp(this.currentRsvpId)
    )
  }


}
