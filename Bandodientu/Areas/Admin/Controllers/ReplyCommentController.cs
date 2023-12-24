using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReplyCommentController : Controller
    {
        int PageSize = 3;
        private readonly DataContext _context;

        public ReplyCommentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.replyComments.Find(id);

            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.replyComments.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.replyComments.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index(int id)
        {
            ViewBag.customer = _context.customers.ToList();
            Function._ReplyCommentID = id;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var items = _context.replyComments.OrderByDescending(m => (m.ReplyCommentID)).Where(m => (m.CommentID == id)).ToList();
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost]
        public IActionResult IsActive(int? id)
        {
            var item = _context.replyComments.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
    }
}
