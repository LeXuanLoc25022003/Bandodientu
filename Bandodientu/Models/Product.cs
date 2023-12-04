using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Bandodientu.Models
{
	[Table("Products")]
	public class Product
	{
		[Key]
		public int ProductID { get; set; }
		public string? ProductName { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal DiscountedPrice { get; set; }
		public string? Description { get; set; }

		public string? Color { get; set; }
		public int Quantity { get; set; }

		public string? DiscountCode { get; set; }
		public string? Image { get; set; }
		public string? New { get; set; }
		public string? Link { get; set; }
		public int MenuID { get; set; }
		public string? Contents {  get; set; }
		public bool? IsActive { get; set; }
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}
