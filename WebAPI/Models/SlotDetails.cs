using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("SlotDetails")]
    public class SlotDetails
    {
        [Key]
        public int SlotId { get; set; }
        [Required]
        public int SlotNumber { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string SlotDate { get; set; }
        [StringLength(250)]
        public string SlotStatus { get; set; } = null!;
        [StringLength(250)]
        public string SlotPriority { get; set; } = null!;
        [Required]
        public bool? IsEnabled { get; set; }
        [Required]
        public string SlotTime { get; set; }
    }
}
