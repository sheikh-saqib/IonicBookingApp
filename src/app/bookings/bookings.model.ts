export class Bookings {
  constructor(
    public id: number,
    public placeId: number,
    public UserId: number,
    public placeName: string
  ) {}
}
export class SlotDetails {
  constructor(
    public SlotId: number,
    public SlotNumber: number,
    public SlotDate: string,
    public SlotTime: string,
    public SlotStatus: string,
    public SlotPriority: string,
    public showDeleteSlot: boolean,
    public Category: any,
    public Venue: any
  ) {}
}
