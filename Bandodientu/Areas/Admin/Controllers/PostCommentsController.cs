using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostCommentsController : Controller
    {
        private readonly DataContext _context;
        public PostCommentsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            Function._PostID = id;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var items = _context.postComments.OrderByDescending(m => (m.CommentID)).Where(m => (m.PostID == id)).ToList();
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }
    }
}
