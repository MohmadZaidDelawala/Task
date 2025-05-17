using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class TokenMaster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int EmployeeID { get; set; }

        public string? Token { get; set; }

        // Navigation Property
        public UserMaster? User { get; set; }
    }
}
