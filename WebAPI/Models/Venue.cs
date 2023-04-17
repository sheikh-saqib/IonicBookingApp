using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("Venue")]
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        [StringLength(50)]
        public string Code { get; set; } = null!;
        [StringLength(250)]
        public string Name { get; set; } = null!;
        [StringLength(2500)]
        public string NavigationLink { get; set; } = null!;
        [StringLength(250)] 
        public string LatLong { get; set; } = null!;
        [StringLength(20)] 
        public string ContactNumber { get; set; } = null!;
        [StringLength(250)]
        public string Address { get; set; } = null!;
        [Required]
        public bool? IsActive { get; set; }
        public City City { get; set; } = null!;
        [Required]
        public int CityId { get; set; }
        public List<Photos> Photos { get; set; }
    }
}
