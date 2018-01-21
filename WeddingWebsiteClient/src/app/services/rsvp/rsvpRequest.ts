import { Guest, Rsvp } from "../../rsvp/rsvp";

export class rsvpRequest {
    constructor(
        rsvp: Rsvp
    ) {
        this.Id = rsvp.id;
        this.ReferenceName = rsvp.referenceName;
        this.NumberOfGuests = rsvp.numberOfGuests;
        this.HasResponded = rsvp.hasResponded;
        this.Guests = rsvp.guests;
    }

    Id: number;
    ReferenceName: string;
    NumberOfGuests: number;
    HasResponded: boolean;
    Guests: Guest[]
}
export class guestRequest {
    constructor(
        guest: Guest
    ) {
        this.GuestRsvpId = guest.guestRsvpId;
        this.FirstName = guest.firstName;
        this.LastName = guest.lastName;
        this.FoodAllergies = guest.foodAllergies;
        this.IsToBeDeleted = guest.isToBeDeleted;
    }
    GuestRsvpId: number
    FirstName: string;
    LastName: string;
    FoodAllergies: string;
    IsToBeDeleted: boolean;
}