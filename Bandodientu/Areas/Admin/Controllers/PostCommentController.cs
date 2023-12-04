using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PostCommentController : Controller
	{
		private readonly DataContext _context;
		public PostCommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var mnList = _context.postComments.OrderBy(m => m.CommentID).ToList();
			return View(mnList);
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.postComments.Find(id);
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
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.postComments.Find(id);
			if (mn == null)
			{
				return NotFound();
			}
			return View(mn);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit(PostComment comment)
		{
			if (ModelState.IsValid)
			{
				_context.postComments.Update(comment);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(comment);
		}
	}
}
