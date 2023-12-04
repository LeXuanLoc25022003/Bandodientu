using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Customer")]
	public class Customer
	{
		[Key]
		public int CustomerID { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Phone { get; set; }
		public string? UserName { get; set; }
		public DateTime Birthday {  get; set; }
		public string? Avater {  get; set; }
		public int LocationID { get; set;}
		public bool? IsActive { get; set; }
	}
}
