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
    public class CommentController : Controller
    {
        private readonly DataContext _context;
        int PageSize = 5;
        public CommentController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int productPage=1)
        {
            ViewBag.postcomment = _context.Products.Where(m => m.IsActive == true).ToList();
            return View(
			new CommentListViewModel
			{
				Comments = _context.Comments
				.Skip((productPage - 1) * PageSize)
				.Take(PageSize),
					PagingInfo = new PagingInfo
					{
						ItemsPerPage = PageSize,
						CurrentPage = productPage,
						TotalItems = _context.Comments.Count()
					}
				}
			);
		}
        [HttpPost]
        public IActionResult IsActive(int? id)
        {
            var item = _context.Comments.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Search(int keywords,int month,int year, int productPage = 1)
        {
            return View("Index",
                new CommentListViewModel
                {
                    Comments = _context.Comments
                    .Where(m => m.ProductID.Equals(keywords) || m.DateTime.Month == month || m.DateTime.Year == year)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Comments.Count()
                    }
                }
                );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Comments
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.CommentID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Comments.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleComment = _context.Comments.Find(id);
            if(deleComment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(deleComment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Comments.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
    }
}
