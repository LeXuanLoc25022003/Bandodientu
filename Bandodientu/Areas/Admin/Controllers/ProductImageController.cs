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
	public class ProductImageController : Controller
	{
		int PageSize = 3;
		private readonly DataContext _context;

		public ProductImageController(DataContext context)
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
            var items = _context.ProductImages.OrderByDescending(m => (m.ProductImageID)).Where(m => (m.ProductID == id)).ToList();
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(ProductImage post)
		{
			if (ModelState.IsValid)
			{
				post.ProductID = Function._ProductID;
				_context.Add(post);
				_context.SaveChanges();
                return RedirectToAction("Index", new { id = Function._ProductID });
            }
            return View(post);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.ProductImages.Find(id);

            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.ProductImages.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.ProductImages.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.ProductImages.Find(id);
			if (mn == null)
			{
				return NotFound();
			}
			return View(mn);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit(ProductImage post)
		{
			if (ModelState.IsValid)
			{
				post.ProductID = Function._ProductID;
				_context.ProductImages.Update(post);
				_context.SaveChanges();
				return RedirectToAction("Index", new { id = Function._ProductID });

            }
			return View(post);
		}
	}
}
