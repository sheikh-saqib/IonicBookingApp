using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("Photos")]
    public class Photos
    {
        [Key]
        public int PhotoId { get; set; }
        public Venue Venue { get; set; } = null!;
        [Required]
        public int VenueId { get; set; }
        public string Image { get; set; } = null!;
    }
}
