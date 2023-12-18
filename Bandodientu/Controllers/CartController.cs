using Bandodientu.Helpers;
using Bandodientu.Infrastructure;
using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Bandodientu.Utilities;

namespace Bandodientu.Controllers
{
	public class CartController : Controller
	{
        private readonly DataContext _context;
		public Cart? Cart { get; set; }

		public CartController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
		}
		public ActionResult AddToCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public ActionResult DeleteToCart(int productId)
		{
			Product? product = _context.Products
				.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, -1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public ActionResult RemoveFromCart(int productId)
		{
			Product? product = _context.Products
				.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
	}
}
