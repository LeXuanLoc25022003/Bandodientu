using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderStatusController : Controller
    {
        private readonly DataContext _context;

        public OrderStatusController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.OrderStatuses.OrderBy(m => m.OrderStatusID).ToList();
            return View(mnList);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderStatuses == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.OrderStatuses
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.OrderStatusID == id);
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
            var mn = _context.OrderStatuses.Find(id);

            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.OrderStatuses.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.OrderStatuses.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.OrderStatuses.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(OrderStatus product)
        {
            if (ModelState.IsValid)
            {
                _context.OrderStatuses.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
