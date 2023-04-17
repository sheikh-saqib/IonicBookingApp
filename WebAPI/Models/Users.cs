using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppAPI.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(250)]
        public string Email { get; set; } = null!;

        [StringLength(250)]
        public string FirstName { get; set; } = null!;

        [StringLength(250)]
        public string LastName { get; set; } = null!; 
        [StringLength(250)]
        public string Role { get; set; } = null!;

        [StringLength(20)]
        public string? Mobile { get; set; }
        [StringLength(250)]
        public string GoogleToken { get; set; } = null!;
    }
}
