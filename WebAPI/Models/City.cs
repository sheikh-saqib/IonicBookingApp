using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [StringLength(50)]
        public string Code { get; set; } = null!;
        [StringLength(250)] 
        public string Name { get; set; } = null!;
    }
}
