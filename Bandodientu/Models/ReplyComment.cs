using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("ReplyComment")]
	public class ReplyComment
	{
		[Key]
		public int ReplyCommentID { get; set; }
		public int CustomerID { get; set; }
		public int CommentID {  get; set; }
		public string? Messeage { get; set; }
		public DateTime CreateDate { get; set; }
		public bool? IsActive { get; set; }
		public Customer Customer { get; set; }
	}
}
