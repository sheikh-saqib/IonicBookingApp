import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RazorpayService {
  constructor(private http: HttpClient) {}

  createOrder(selectedSlotIds: any): Observable<any> {
    var ordersPayload = {
      SlotIds: selectedSlotIds,
    };
    return this.http.post(environment.baseUrl + 'orders', ordersPayload);
  }

  // capturePayment(
  //   _response: any,
  //   amount: number,
  //   selectedDate: any,
  //   selectedSlotIds: any,
  //   orderDetails: any
  // ) {
  //   debugger;
  //   console.log(amount);
  //   console.log(_response);
  //   var BookingPayload = {
  //     SlotDate: selectedDate,
  //     RazorPayOrderId: orderDetails.orderId,
  //     RazorPayPaymentId: '_response.razorpay_payment_id',
  //     RazorPaySignature: '_response.razorpay_signature',
  //     Amount: amount,
  //     SlotIds: selectedSlotIds,
  //   };
  //   return this.http
  //     .post(environment.baseUrl + 'PaymentGateway', BookingPayload)
  //     .subscribe((res) => {
  //       var aa = res;
  //     });
  //   // return this.http.post('/api/capture-payment', { paymentId, amount });
  // }
}
