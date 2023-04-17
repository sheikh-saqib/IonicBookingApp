import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {
  AlertController,
  IonAccordionGroup,
  ModalController,
} from '@ionic/angular';
import { RazorpayService } from 'src/app/services/razorpay.service';
import { environment } from 'src/environments/environment';
import { SlotDetails } from '../bookings.model';
declare var Razorpay: any;
@Component({
  selector: 'app-create-booking',
  templateUrl: './create-booking.component.html',
  styleUrls: ['./create-booking.component.scss'],
})
export class CreateBookingComponent implements OnInit {
  @Input() selectedPlace: any;
  @ViewChild(IonAccordionGroup) accordionGroup: IonAccordionGroup | any;
  startDate: any;
  selectedDate: any;
  slideValue: number = 0;
  showCalender: boolean = true;
  slotDetails: SlotDetails[] = [];
  totalSlotPrice: number = 0;
  totalConveniencePrice: number = 0;
  totalDiscountPrice: number = 0;
  payableAmount: number = 0;
  orderDetails: any;
  selectedSlotIds: number[] = [];
  constructor(
    private modalCtrl: ModalController,
    private http: HttpClient,
    private datePipe: DatePipe,
    private razorpayService: RazorpayService,
    private alertCtrl: AlertController
  ) {}

  ngOnInit() {
    this.startDate = new Date();
    console.log(this.selectedPlace);
  }

  onCancel() {
    this.modalCtrl.dismiss(null, 'cancel');
  }

  onDatePick() {
    this.accordionGroup.value = '';
    this.startDate = this.selectedDate;
    this.getSlotDetails();
  }
  redirectToPayment() {
    if (this.slideValue == 100) {
      this.slideValue = 0;
      this.modalCtrl.dismiss();
      this.razorpayService
        .createOrder(this.selectedSlotIds)
        .subscribe((res: any) => {
          this.orderDetails = res;
          var options = {
            key: 'rzp_test_JOC0wRKpLH1cVW', // Enter the Key ID generated from the Dashboard
            amount: this.orderDetails.amount, // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise
            currency: this.orderDetails.currency,
            name: this.orderDetails.name,
            description: this.orderDetails.description,
            order_id: this.orderDetails.orderId,
            handler: (response: any) => {
              console.log(response); //this returns the expected value
              this.handle_response(response); //does not work as cannot identify 'this'
            },
            prefill: {
              name: this.orderDetails.name,
              email: this.orderDetails.email,
              contact: this.orderDetails.phoneNumber,
            },
            theme: {
              color: '#3399cc',
            },
          };
          var rzp1 = new Razorpay(options);
          rzp1.on(
            'payment.failed',
            (response: {
              error: {
                code: any;
                description: any;
                source: any;
                step: any;
                reason: any;
                metadata: { order_id: any; payment_id: any };
              };
            }) => {
              alert('Payment failed please try again .... ');
              console.log(response);
            }
          );
          rzp1.open();
        });
    }
  }
  handle_response(_response: any) {
    var BookingPayload = {
      SlotDate: this.selectedDate,
      RazorPayOrderId: _response.razorpay_order_id,
      RazorPayPaymentId: _response.razorpay_payment_id,
      RazorPaySignature: _response.razorpay_signature,
      Amount: this.payableAmount,
      SlotIds: this.selectedSlotIds,
      VenueId: this.selectedPlace.id,
    };
    this.http
      .post(environment.baseUrl + 'PaymentGateway', BookingPayload, {
        responseType: 'text',
      })
      .subscribe((res) => {
        if (res == 'Success') {
          this.presentAlert('Payment Processed Successfully !!!!');
        } else {
          this.presentAlert(
            'Some error occured while processing payment process !!!'
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
  onAddSlot(slotDetails: any) {
    this.selectedSlotIds.push(slotDetails.SlotId);
    const filteredSlot = this.slotDetails.find(
      (obj) => obj.SlotId === slotDetails.SlotId
    );
    if (filteredSlot) {
      filteredSlot.showDeleteSlot = true;
      this.totalSlotPrice = this.totalSlotPrice + slotDetails.Category.amount;
      this.totalConveniencePrice =
        this.totalConveniencePrice + slotDetails.Category.covenienceFee;
      this.totalDiscountPrice =
        this.totalDiscountPrice + slotDetails.Category.discount;
      this.payableAmount =
        this.totalSlotPrice +
        this.totalConveniencePrice -
        this.totalDiscountPrice;
    }
  }
  onDeleteSlot(slotDetails: any) {
    this.selectedSlotIds.splice(slotDetails.SlotId);
    const filteredSlot = this.slotDetails.find(
      (obj) => obj.SlotId === slotDetails.SlotId
    );
    if (filteredSlot) {
      filteredSlot.showDeleteSlot = false;
      this.totalSlotPrice = this.totalSlotPrice - slotDetails.Category.amount;
      this.totalConveniencePrice =
        this.totalConveniencePrice - slotDetails.Category.covenienceFee;
      this.totalDiscountPrice =
        this.totalDiscountPrice - slotDetails.Category.discount;
      this.payableAmount =
        this.totalSlotPrice +
        this.totalConveniencePrice -
        this.totalDiscountPrice;
    }
  }
  getSlotDetails() {
    this.slotDetails = [];
    this.http
      .get(
        environment.baseUrl +
          'SlotDetails/' +
          this.datePipe.transform(this.selectedDate, 'dd-MM-yyyy')
      )
      .subscribe((res) => {
        const slotDetails = JSON.parse(JSON.stringify(res));
        for (const row of slotDetails) {
          const slotData = new SlotDetails(
            row.slotId,
            row.slotNumber,
            row.slotDate,
            row.slotTime,
            row.slotStatus,
            row.slotPriority,
            false,
            row.category,
            row.category.venue
          );
          this.slotDetails.push(slotData);
        }
        console.log(this.slotDetails);
        this.showCalender = false;
      });
  }
}
