export class Rsvp {
    constructor(
        id: number,
        referenceName: string,
        numberOfGuests: number,
        hasResponded: boolean,
        guests: Guest[]) {
            this.id = id;
            this.referenceName = referenceName;
            this.numberOfGuests = numberOfGuests;
            this.hasResponded = hasResponded;
            
            this.guests = guests ? guests : new Array<Guest>();
            this.savedGuests = new Array<Guest>();
            this.guests.forEach(guest => this.savedGuests.push(guest))

            this.fillInEmptyGuests(); 
        }

    fillInEmptyGuests() {
        var existingGuestIds = this.guests.map(guest => guest.guestRsvpId);
        for (var i = 0; i < this.numberOfGuests; i++) {
            if (!(existingGuestIds.findIndex(id => id == i) > -1)){
                let guest = new Guest()
                guest.guestRsvpId = i;
                this.guests.push(guest)
            }
        }
    }
    id: number;
    referenceName: string;
    numberOfGuests: number;
    hasResponded: boolean;
    guests: Array<Guest>;
    savedGuests: Array<Guest>;

    public numberOfGuestsRemaining() {
        
        return this.savedGuests 
            ? this.numberOfGuests - this.savedGuests.length 
            : this.numberOfGuests
    };
}
export class Guest {
    guestRsvpId: number
    firstName: string;
    lastName: string;
    foodAllergies: string;
    isToBeDeleted: boolean;
}