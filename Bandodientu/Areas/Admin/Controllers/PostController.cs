using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PostController : Controller
	{
		int PageSize = 3;
		private readonly DataContext _context;

		public PostController(DataContext context)
		{
			_context = context;
		}

		public IActionResult Index(int productPage = 1)
		{
            return View(
            new PostListViewModel
            {
                Posts = _context.Posts
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.Posts.Count()
                }
            }
            );
        }
        [HttpPost]
        public IActionResult IsActive(int? id)
        {
            var item = _context.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Search(int month, int productPage = 1)
        {
            return View("Index",
                new PostListViewModel
                {
                    Posts = _context.Posts
                    .Where(m => m.MenuID.Equals(month))
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Posts.Count()
                    }
                }
                );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Posts
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "---Select---",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View();
        }

        [HttpPost]

        public IActionResult Create(Post post)
		{
			if (ModelState.IsValid)
			{
				_context.Add(post);
				_context.SaveChanges();
			}
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.Posts.Find(id);

			if (mn == null)
			{
				return NotFound();
			}
			return View(mn);
		}

		[HttpPost]

		public IActionResult Delete(int id)
		{
			var deleMenu = _context.Posts.Find(id);
			if (deleMenu == null)
			{
				return NotFound();
			}
			_context.Posts.Remove(deleMenu);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.Posts.Find(id);
			if (mn == null)
			{
				return NotFound();
			}
			var mnList = (from m in _context.Menus
						  select new SelectListItem()
						  {
							  Text = m.MenuName,
							  Value = m.MenuID.ToString()
						  }).ToList();
			mnList.Insert(0, new SelectListItem()
			{
				Text = "---Select---",
				Value = string.Empty
			});
			ViewBag.mnList = mnList;
			return View(mn);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit(Post post)
		{
			if (ModelState.IsValid)
			{
				_context.Posts.Update(post);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(post);
		}
	}
}
