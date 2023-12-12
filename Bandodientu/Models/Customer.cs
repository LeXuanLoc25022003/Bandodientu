using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Customer")]
	public class Customer
	{
		[Key]
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		public string? Password { get; set; }
		[Required(ErrorMessage = "Phone is required.")]
		public string? Phone { get; set; }
		[Required(ErrorMessage = "UserName is required.")]
		public string? UserName { get; set; }
		public DateTime Birthday {  get; set; }
		public string? Avater {  get; set; }
		public int LocationID { get; set;}
		public bool? IsActive { get; set; }
	}
}
