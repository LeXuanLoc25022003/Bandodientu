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
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.postComments
                .Where(m => m.CommentID == id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleComment = _context.postComments.Find(id);
            if (deleComment == null)
            {
                return NotFound();
            }
            _context.postComments.Remove(deleComment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
