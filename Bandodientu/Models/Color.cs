using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Color")]
	public class Color
	{
		[Key]
		public int ColorID {  get; set; }
		public bool? IsActive { get; set; }
		public string? Name { get; set; }
	}
}
