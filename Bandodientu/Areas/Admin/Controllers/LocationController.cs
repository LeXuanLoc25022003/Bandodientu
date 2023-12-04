using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly DataContext _context;
        public LocationController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.Locations.OrderBy(m => m.LocationID).ToList();
            return View(mnList);
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
