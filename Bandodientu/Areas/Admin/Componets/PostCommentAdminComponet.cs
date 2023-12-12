using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Components
{
    [ViewComponent(Name = "PostCommentAdmin")]
    public class PostCommentAdminComponet : ViewComponent
    {
        private readonly DataContext _context;
        public PostCommentAdminComponet(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.postComments
                          where (mn.IsActive == true)
                          select mn).Take(4).ToList();
            ViewBag.comment = _context.postComments.Count(m=>m.IsActive == true).ToString();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
