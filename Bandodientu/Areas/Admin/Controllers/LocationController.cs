using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly DataContext _context;
        int PageSize = 5;
        public LocationController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Locations
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        public IActionResult Index(int productPage = 1)
        {
            return View(
            new LocationListViewModel
            {
                Locations = _context.Locations
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.Locations.Count()
                }
            }
            );
        }
        [HttpPost]
        public IActionResult IsActive(int? id)
        {
            var item = _context.Locations.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            return View("Index",
                new LocationListViewModel
                {
                    Locations = _context.Locations
                    .Where(m => m.Name.Equals(keywords))
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Locations.Count()
                    }
                }
                );
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Locations.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Locations.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Location mn)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Locations.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Update(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }
    }
}
