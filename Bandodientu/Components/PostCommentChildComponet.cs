using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "PostCommentChild")]
	public class PostCommentChildComponent : ViewComponent
	{
		private readonly DataContext _context;
		public PostCommentChildComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.replyComments
								 where (p.IsActive == true)
								 orderby p.ReplyCommentID descending
								 select p).Take(6).ToList();
			ViewBag.Customer = _context.customers.ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
