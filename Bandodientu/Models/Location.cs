using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Location")]
	public class Location
	{
		[Key]
		public int LocationID { get; set; }
		public string? Name { get; set; }
		public int Parent {  get; set; }
		public bool? IsActive { get; set; }
	}
}
