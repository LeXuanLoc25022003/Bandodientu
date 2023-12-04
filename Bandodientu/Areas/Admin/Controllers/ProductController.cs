using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.Products.OrderBy(m=>m.ProductID).ToList();
            return View(mnList);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Products.Find(id);

            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Products.Find(id);
            if(deleMenu == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
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

        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Products.Find(id);
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

        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
