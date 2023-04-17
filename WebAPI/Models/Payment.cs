using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("PaymentDetails")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public int VenueId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string RazorPayPaymentId { get; set; } = null!;
        public string RazorPayOrderId { get; set; } = null!;
        public Booking BookingDetails { get; set; }
        public Venue VenueDetails { get; set; }
    }

}
