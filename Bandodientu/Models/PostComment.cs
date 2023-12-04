using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
	[Table("PostComment")]
	public class PostComment
	{
		[Key]
		public int CommentID { get; set; }
		public string? Name {  get; set; }
		public string? Email {  get; set; }
		public DateTime CreateDate { get; set; }
		public int? PostID {  get; set; }
		public bool? IsActive { get; set; }
		public string? Message {  get; set; }
		public virtual Post? Post { get; set; }
	}
}
