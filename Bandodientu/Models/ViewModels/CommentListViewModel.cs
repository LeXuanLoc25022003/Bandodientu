namespace Bandodientu.Models.ViewModels
{
	public class CommentListViewModel
	{
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
