using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        int PageSize = 5;
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int productPage = 1)
        {
            return View(
            new OrderListViewModel
            {
                Orders = _context.Orders
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.Orders.Count()
                }
            }
            );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
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
            var mn = _context.Orders.Find(id);

            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Orders.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            var mnList = (from m in _context.OrderStatuses
                          select new SelectListItem()
                          {
                              Text = m.Name,
                              Value = m.OrderStatusID.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "---Select---",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Orders.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Order product)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
