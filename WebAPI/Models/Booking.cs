using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public string SlotDate { get; set; }
        //public Users Users { get; set; }
        [Required]
        public int UserId { get; set; }
        [StringLength(250)]
        public string BookingStatus { get; set; } = null!;
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string RazorPayPaymentId { get; set; } = null!;
        public string RazorPayOrderId { get; set; } = null!;
    }
}
