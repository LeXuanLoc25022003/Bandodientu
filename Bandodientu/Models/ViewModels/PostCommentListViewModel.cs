namespace Bandodientu.Models.ViewModels
{
	public class PostCommentListViewModel
	{
        public IEnumerable<PostComment> PostComments { get; set; } = Enumerable.Empty<PostComment>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
