using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("Comment")]
	public class Comment
	{
		[Key]

		public int CommentID { get; set; }
		public string? YourName { get; set; }

		public string? YourEmail { get; set; }
		public DateTime? DateTime { get; set; }

		public string? Review { get; set; }

		public int ProductID { get; set; }
		public int Rate { get; set; }
		public bool? IsActive { get; set; }
	}
}
