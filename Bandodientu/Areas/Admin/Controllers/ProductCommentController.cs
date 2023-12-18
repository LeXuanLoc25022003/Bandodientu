using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCommentController : Controller
    {
        private readonly DataContext _context;
        public ProductCommentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            Function._ProductID = id;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var items = _context.Comments.OrderByDescending(m => (m.CommentID)).Where(m => (m.ProductID == id)).ToList();
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }
    }
}
