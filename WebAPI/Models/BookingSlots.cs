using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("BookingSlots")]
    public class BookingSlots
    {
        [Key]
        public int BookingSlotId { get; set; }
        public int BookingId { get; set; }
        public int SlotId { get; set; }
    }
}
