using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Models
{
    public class ProductMaster
    {
        [Key]
        public int GroupID { get; set; }

        public string Name { get; set; }
        public decimal MRP { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public int AddedBy { get; set; }
        public UserMaster? AddedByUser { get; set; }
    }
}
