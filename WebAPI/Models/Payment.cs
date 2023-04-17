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
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string RazorPayPaymentId { get; set; } = null!;
        public string RazorPayOrderId { get; set; } = null!;
    }
}
