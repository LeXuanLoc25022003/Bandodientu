using Bandodientu.Infrastructure;
using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bandodientu.Controllers
{
    public class CheckoutController : Controller
    {
		public readonly DataContext _context;

        public CheckoutController(DataContext context)
		{
			_context = context;
		}
        public IActionResult Index()
        {
			if(!Function.IsLoginCustomer())
				return RedirectToAction("Dangnhap", "Home");
			return View("Index", HttpContext.Session.GetJson<Cart>("cart"));
		}
		[HttpPost]
		public bool Order(string address)
		{
			try
			{
				Customer? customer = _context.customers.FirstOrDefault(c => c.CustomerID == Function._CustomerID);
				Cart? cart = HttpContext.Session.GetJson<Cart>("cart");
				
				if (cart==null)
				{
					return false;
				}
					Order Order = new Order();
					Random rd = new Random();
					Order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
					Order.CustomerID = customer.CustomerID;
					Order.Address = address;
					Order.OrderStatusID = 1;
					Order.ToTalAmount = cart.ComputeTotalValue();
					Order.CreateDate = DateTime.Now;
					_context.Orders.Add(Order);
					_context.SaveChanges();
					var OrderId = _context.Orders.FirstOrDefault(m => (m.Code == Order.Code) && (m.CreateDate == Order.CreateDate));
					foreach (var item in cart.Lines)
					{
						OrderDetail orderDetail = new OrderDetail
						{
							OrderID = Order.OrderID,
							ProductID = item.Product.ProductID,
							Price = item.Product.DiscountedPrice,
							Quantity = item.Quantity,
						};

						_context.OrderDetails.Add(orderDetail);
					}
					_context.SaveChanges();
					cart.Clear();
					HttpContext.Session.SetJson("cart", cart);
					return true;
			}
			catch
			{
				return false;
			}
        }
        //public IActionResult Cancel(int id)
        //{
        //	var items = _context.Orders.Find(id);
        //	return View(items);
        //}
        //[HttpPost]
        //public IActionResult Cancel(Order order)
        //{
        //	if(ModelState.IsValid)
        //	{
        //		order.OrderStatusID = 5;
        //		order.CreateDate = DateTime.Now;
        //		_context.Orders.Update(order);
        //		_context.SaveChanges();
        //		return RedirectToAction("Index","Home");
        //	}
        //	return View(order);
        //}
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
    }
}
