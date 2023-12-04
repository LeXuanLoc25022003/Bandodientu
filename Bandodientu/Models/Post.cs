using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public string? Title { get; set; }
        public string? Abstract { get; set; }
        public string? Contents { get; set; }
        public string? Images { get; set; }
        public string? Link { get; set; }
        public string? Author { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsActive { get; set; }
        public int PostOrder { get; set; }
        public int MenuID { get; set; }
        public int Status { get; set; }
	}
}
