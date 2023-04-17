using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Context;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using System.Data;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IDataRepository _repo;
        public OrdersController(DataContext context, IDataRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        [Authorize]
        [HttpPost]
        public async Task<PaymentGateway> Post([FromBody] Orders ordersPayload)
        {

            try
            {
                float slotsAmount = 0;
                foreach (var SlotId in ordersPayload.SlotIds)
                {
                    Expression<Func<SlotDetails, bool>> filter = null;
                    filter = a => a.SlotId == SlotId;
                    var slotDetails = await _repo.FindAllAsync<SlotDetails>(filter, null);
                    slotDetails[0].Category = await _repo.GetById<Category>(slotDetails[0].CategoryId);
                    slotsAmount = (slotsAmount + slotDetails[0].Category.Amount + slotDetails[0].Category.CovenienceFee) - slotDetails[0].Category.Discount;
                }
                var userEmail = HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var userName = HttpContext.User.Claims.Last(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var userPhone = HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone").Value;
                // Generate random receipt number for order
                Random randomObj = new Random();
                string transactionId = randomObj.Next(10000000, 100000000).ToString();
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_JOC0wRKpLH1cVW", "9EzSlxvJbTyQ2Hg0Us5ZX4VD");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", slotsAmount * 100);
                options.Add("receipt", transactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual
                                                     //options.Add("Notes", "Test Payment of Merchant");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();
                PaymentGateway order = new PaymentGateway
                {
                    OrderId = orderResponse.Attributes["id"],
                    RazorpayKey = "rzp_test_8P7RhnsImxd2OR",
                    Amount = slotsAmount * 100,
                    Currency = "INR",
                    Name = userName,
                    Email = userEmail,
                    PhoneNumber = userPhone,
                    Description = "Order to  Merchant"
                };
                return await Task.FromResult(order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
