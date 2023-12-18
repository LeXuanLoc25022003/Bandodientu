using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string? Image { get; set; }
        public bool? IsActive { get; set; }

    }
}
