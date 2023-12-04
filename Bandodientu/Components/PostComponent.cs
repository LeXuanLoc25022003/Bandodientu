using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
    [ViewComponent(Name = "Post")]
    public class PostComponent : ViewComponent
    {
        private readonly DataContext _context;

        public PostComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofComment = (from post in _context.Posts
                                 where (post.IsActive == true)
                                 select post).Take(3).ToList();
			ViewBag.postcomments = _context.postComments.Where(m => (bool)m.IsActive).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofComment));
        }
    }
}
