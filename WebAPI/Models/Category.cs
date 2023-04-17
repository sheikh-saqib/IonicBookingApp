using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [StringLength(50)]
        public string Code { get; set; } = null!;
        [StringLength(250)]
        public string Name { get; set; } = null!;
        public Venue Venue { get; set; } =  null!;
        [Required]
        public int VenueId { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public float Discount { get; set; }
        public float CovenienceFee { get; set; }
        [StringLength(250)]
        public string Prority { get; set; } = null!;
       
    }
}
