using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("IsRead")]
	public class IsReadl
	{
		[Key]
		public int IsRead { get; set; }
		public string? Name { get; set; }
	}
}
