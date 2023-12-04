using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bandodientu.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		[Key]
		public int OrderDetailID {  get; set; }
		public int ProductID { get; set; }
		public int OrderID {  get; set; }
		public decimal Price {  get; set; }
		public int Quantity {  get; set; }
	}
}
