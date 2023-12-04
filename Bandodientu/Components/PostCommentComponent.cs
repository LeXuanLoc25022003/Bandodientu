using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "PostComment")]
	public class PostCommentComponent : ViewComponent
	{
		private readonly DataContext _context;
		public PostCommentComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.postComments
								 where (p.IsActive == true)
								 orderby p.PostID descending
								 select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
