using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.DTOs
{
    public class PaymentGateway
    {
        public string OrderId { get; set; }
        public string RazorpayKey { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
