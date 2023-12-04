using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "Comment")]
	public class CommentComponent : ViewComponent
	{
		private readonly DataContext _context;

		public CommentComponent(DataContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofComment = (from comment in _context.Comments
								 where(comment.IsActive==true)
								 select comment).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofComment));
		}
	}
}
