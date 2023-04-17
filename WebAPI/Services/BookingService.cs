
using MyAppAPI.Context;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace MyAppAPI.Services
{
    public class BookingService
    {
        private readonly IDataRepository _repo;
        public BookingService(DataContext context, IDataRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> CreateBooking(BookingDTO bookingPayload, string userId)
        {
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_JOC0wRKpLH1cVW", "9EzSlxvJbTyQ2Hg0Us5ZX4VD");
                Razorpay.Api.Order orderResponse = client.Order.Fetch(bookingPayload.RazorPayOrderId);
                Razorpay.Api.Payment paymentResponse = client.Payment.Fetch(bookingPayload.RazorPayPaymentId);

                JToken paymentResponseJT = ((Newtonsoft.Json.Linq.JToken)paymentResponse.Attributes).Root;
                Root paymentResponseDetails = paymentResponseJT.ToObject<Root>();

                //Booking Entry
                Booking bookingDetails = new Booking()
                {
                    SlotDate = bookingPayload.SlotDate,
                    Amount = bookingPayload.Amount,
                    UserId = Convert.ToInt32(userId),
                    BookingStatus ="Booked",
                    BookingDate = DateTime.Now,
                    BookingUpdateDate = DateTime.Now,
                    RazorPayOrderId = bookingPayload.RazorPayOrderId,
                    RazorPayPaymentId = bookingPayload.RazorPayPaymentId
                };
                await _repo.Create(bookingDetails);

                //Booking Slots Entry
                Expression<Func<Booking, bool>> filter = null;
                filter = a => a.RazorPayOrderId == bookingPayload.RazorPayOrderId && a.RazorPayPaymentId == bookingPayload.RazorPayPaymentId;
                var bookingData = await _repo.FindAllAsync<Booking>(filter, null);
                if (bookingData != null)
                {
                    foreach (var item in bookingPayload.SlotIds)
                    {
                        BookingSlots bookingSlotsDetails = new BookingSlots()
                        {
                            SlotId = item,
                            BookingId = bookingData[0].BookingId
                        };
                        await _repo.Create(bookingSlotsDetails);
                    }
                }
                //Payment Entry
                if (paymentResponseDetails.status == "captured")
                {
                    Models.Payment paymentDetails = new Models.Payment()
                    {
                        BookingId = bookingData[0].BookingId,
                        PaymentDate = DateTime.Now,
                        PaymentStatus = paymentResponseDetails.status,
                        Amount = bookingPayload.Amount,
                        UserId = Convert.ToInt32(userId),
                        RazorPayOrderId = bookingPayload.RazorPayOrderId,
                        RazorPayPaymentId = bookingPayload.RazorPayPaymentId,
                        VenueId = bookingPayload.VenueId
                    };
                    await _repo.Create(paymentDetails);
                }
                //Slots Status Update
                foreach (var item in bookingPayload.SlotIds)
                {
                    var existingSlotDetails = await _repo.GetById<SlotDetails>(item);
                    if (existingSlotDetails != null)
                    {
                        existingSlotDetails.SlotStatus = "Booked";
                        await _repo.Update(existingSlotDetails);
                    }
                }
                //scope.Complete();
                return "Success";
                //} 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public async Task<string> CancelBooking(int bookingId)
        {
            try
            {
                //Update Booking 
                var existingBookingDetails = await _repo.GetById<Booking>(bookingId);
                if (existingBookingDetails != null)
                {
                    existingBookingDetails.BookingStatus = "Cancelled";
                    existingBookingDetails.BookingUpdateDate = DateTime.Now;
                    await _repo.Update(existingBookingDetails);

                    // Get Slots Associated with Booking
                    Expression<Func<BookingSlots, bool>> filter = null;
                    filter = a => a.BookingId == bookingId;
                    var slots = await _repo.FindAllAsync<BookingSlots>(filter, null);

                    //Slots Status Update
                    foreach (var item in slots)
                    {
                        var existingSlotDetails = await _repo.GetById<SlotDetails>(item.SlotId);
                        if (existingSlotDetails != null)
                        {
                            existingSlotDetails.SlotStatus = "Available";
                            await _repo.Update(existingSlotDetails);
                        }
                    }

                    //payment Status Update
                    Expression<Func<Payment, bool>> payfilter = null;
                    payfilter = a => a.BookingId == bookingId;
                    var paymentDetails = await _repo.FindAllAsync<Payment>(payfilter, null);
                    if (paymentDetails != null || paymentDetails.Count>0)
                    {
                        paymentDetails[0].PaymentStatus = "Refund Requested";
                        await _repo.Update(paymentDetails[0]);
                    }
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
