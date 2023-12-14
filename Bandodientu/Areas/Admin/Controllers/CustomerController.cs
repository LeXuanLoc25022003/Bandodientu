using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        int PageSize = 5;
        public CustomerController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int productPage = 1)
        {
            return View(
            new CustomerListViewModel
            {
                Customers = _context.customers
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.customers.Count()
                }
            }
            );
        }
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            return View("Index",
                new CustomerListViewModel
                {
                    Customers = _context.customers
                    .Where(m => m.UserName.Equals(keywords))
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.customers.Count()
                    }
                }
                );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.customers == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.customers
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null && id == 0)
            {
                return NotFound();
            }
            var mn = _context.customers.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.customers.Find(id);
            if(deleMenu == null)
            {
                return NotFound();
            }
            _context.customers.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            var mnList = (from m in _context.Locations
                          select new SelectListItem()
                          {
                              Text = m.Name,
                              Value = m.LocationID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "---Select---",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.customers.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Customer comment)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
    }
}
