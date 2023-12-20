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
        public async Task<IActionResult> Search(int month, int year)
        {
            return View("Index",
                new OrderListViewModel
                {
                    Orders = _context.Orders
                    .Where(m=>m.CreateDate.Month == month || m.CreateDate.Year == year)
                }
                );
        }
        public IActionResult Detail(int id)
        {
            ViewBag.product = _context.Products.ToList();
            ViewBag.order = _context.Orders.ToList();
            var items = _context.OrderDetails.Where(m=>m.OrderID == id).ToList();
            return View(items);
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
            ViewBag.order = _context.Orders.ToList();
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
        public IActionResult Confirm()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m=>m.OrderStatusID==1).ToList();
            return View(items);
        }
        //[HttpPost]
        //public IActionResult UpdateTT(int id,int trangthai)
        //{
        //    var item = _context.Orders.Find(id);
        //    if (item != null)
        //    {
        //        _context.Orders.Attach(item);
        //        item.OrderStatusID = trangthai;
        //        item.CreateDate= DateTime.Now;
        //        _context.Entry(item).Property(x=>x.OrderStatusID).IsModified =true;
        //        _context.SaveChanges();
        //        return Json(new { messeage = "Success", Success = true });
        //    }
        //    return Json(new {messeage="Success" ,Success=false});
        //}
        public IActionResult Prepar()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m => m.OrderStatusID == 2).ToList();
            return View(items);
        }
        //[HttpPost]
        //public IActionResult UpdateTTDh(int id, int trangthai)
        //{
        //    var item = _context.Orders.Find(id);
        //    if (item != null)
        //    {
        //        _context.Orders.Attach(item);
        //        item.OrderStatusID = trangthai;
        //        item.CreateDate = DateTime.Now;
        //        _context.Entry(item).Property(x => x.OrderStatusID).IsModified = true;
        //        _context.SaveChanges();
        //        return Json(new { messeage = "Success", Success = true });
        //    }
        //    return Json(new { messeage = "Success", Success = false });
        //}
        public IActionResult Deliver()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m => m.OrderStatusID == 3).ToList();
            return View(items);
        }
        //[HttpPost]
        //public IActionResult UpdateDeliver(int id, int trangthai)
        //{
        //    var item = _context.Orders.Find(id);
        //    if (item != null)
        //    {
        //        _context.Orders.Attach(item);
        //        item.OrderStatusID = trangthai;
        //        item.CreateDate = DateTime.Now;
        //        _context.Entry(item).Property(x => x.OrderStatusID).IsModified = true;
        //        _context.SaveChanges();
        //        return Json(new { messeage = "Success", Success = true });
        //    }
        //    return Json(new { messeage = "Success", Success = false });
        //}
        public IActionResult Complete()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m => m.OrderStatusID == 4).ToList();
            return View(items);
        }
        public IActionResult Cancel()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m => m.OrderStatusID == 5).ToList();
            return View(items);
        }
        [HttpPost]
        public IActionResult UpdateCancel(int id, int trangthai)
        {
            var item = _context.Orders.Find(id);
            if (item != null)
            {
                _context.Orders.Attach(item);
                item.OrderStatusID = trangthai;
                item.CreateDate = DateTime.Now;
                _context.Entry(item).Property(x => x.OrderStatusID).IsModified = true;
                _context.SaveChanges();
                return Json(new { messeage = "Success", Success = true });
            }
            return Json(new { messeage = "Success", Success = false });
        }
        public IActionResult CustomerCancel()
        {
            ViewBag.orderstatus = _context.OrderStatuses.ToList();
            ViewBag.customer = _context.customers.ToList();
            var items = _context.Orders.Where(m=>m.OrderStatusID == 6).ToList();
            return View(items);
        }
    }
}
