using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class UserMaster
    {
        [Key]
        public int EmployeeID { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }

        // Navigation Properties
        public ICollection<TokenMaster> Tokens { get; set; }
        public ICollection<ProductMaster> ProductsAdded { get; set; }
    }


}
