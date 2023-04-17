import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AlertController, IonItemSliding } from '@ionic/angular';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.page.html',
  styleUrls: ['./bookings.page.scss'],
})
export class BookingsPage implements OnInit {
  myActiveBookings: any;
  myPastBookings: any;
  selectedSegment: any = 'active';
  constructor(private http: HttpClient, private alertCtrl: AlertController) {}

  ngOnInit() {
    this.myActiveBookings = [];
    this.myPastBookings = [];
    this.http
      .get(environment.baseUrl + 'Payment/GetByUserId')
      .subscribe((res) => {
        var bookingData = JSON.parse(JSON.stringify(res));
        for (const row of bookingData) {
          if (new Date(row.bookingDetails.slotDate) > new Date()) {
            const activeBookingDetails = {
              Amount: row.amount,
              PaymentStatus: row.paymentStatus,
              VenueAddress: row.venueDetails.address,
              VenueName: row.venueDetails.name,
              BookingId: row.bookingDetails.bookingId,
              BookingSlotDateTime: new Date(row.bookingDetails.slotDate),
              BookingDate: row.bookingDetails.bookingDate,
            };
            this.myActiveBookings.push(activeBookingDetails);
          } else {
            const pastBookingDetails = {
              Amount: row.amount,
              PaymentStatus: row.paymentStatus,
              VenueAddress: row.venueDetails.address,
              VenueName: row.venueDetails.name,
              BookingId: row.bookingDetails.bookingId,
              BookingSlotDateTime: new Date(row.bookingDetails.slotDate),
              BookingDate: row.bookingDetails.bookingDate,
            };
            this.myPastBookings.push(pastBookingDetails);
          }
        }
      });
  }
  onCancelBooking(bookingId: number) {
    //cancel bookings with bookingId
    console.log(bookingId);
    this.http
      .post(environment.baseUrl + 'Booking/CancelBooking', bookingId, {
        responseType: 'text',
      })
      .subscribe((res) => {
        if (res == 'Success') {
          this.presentAlert('Booking cancelled successfully !!!');
        } else {
          this.presentAlert(
            'Something went wrong please contact the venue manager !!!'
          );
        }
      });
  }
  async presentAlert(msg: any) {
    const alert = await this.alertCtrl.create({
      header: 'Alert',
      message: msg,
      buttons: ['OK'],
    });
    await alert.present();
  }
  onSegmentChanged(e: any) {
    this.selectedSegment = e.detail.value;
  }
}
