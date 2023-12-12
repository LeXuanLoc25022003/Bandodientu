using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("tbOrder")]
	public class Order
	{
		[Key]
		public int OrderID { get; set; }
		public int CustomerID {  get; set; }
		public string? Address {  get; set; }
		public decimal ToTalAmount {  get; set; }
		public int Quantity {  get; set; }
		public string? Code {  get; set; }
		public int OrderStatusID {  get; set; }
		public DateTime? CreateDate {  get; set; }

	}
}
