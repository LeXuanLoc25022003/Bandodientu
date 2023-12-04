using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("OrderStatus")]
	public class OrderStatus
	{
		[Key]
		public int OrderStatusID { get; set; }
		public string? Name {  get; set; }
		public string? Description { get; set; }
	}
}
