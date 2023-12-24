using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Contact")]
	public class Contact
	{
		[Key]
		public int ContactID { get; set; }
		public string? Name { get; set; }
		public string? Phone {  get; set; }
		public string? Email { get; set; }
		public DateTime CreateDate { get; set; }
		public string? Messeage {  get; set; }
		public bool? IsActive { get; set; }
		public int IsRead {  get; set; }
	}
}
