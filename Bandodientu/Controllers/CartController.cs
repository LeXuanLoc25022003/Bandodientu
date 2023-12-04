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
  //      public IActionResult Checkout()
  //      {
  //         return View();
  //      }
		//[HttpPost]
  //      public IActionResult Checkout(Customer user)
		//{
  //          var tk = user.Email;
  //          var mk = user.Password;
  //          var check = _context.customers.SingleOrDefault(x => x.Email.Equals(tk) && x.Password.Equals(mk));
  //          if (check == null)
  //          {
  //              Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
  //              Function._Phone = string.IsNullOrEmpty(check.Phone) ? string.Empty : check.Phone;
  //              return RedirectToAction("Cart","Checkout");
  //          }
  //          else
  //          {
  //              ViewBag.LoginFail = "Đăng nhập thất bại";
  //              return View("Dangnhap");
  //          }
  //      }
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

				return RedirectToAction("Index", "Home");
			}
		//public List<Cart> Carts
		//{
		//	get
		//	{
		//		var data = HttpContext.Session.Get<List<Cart>>("GioHang");
		//		if (data == null)
		//		{
		//			data = new List<Cart>();
		//		}
		//		return data;
		//	}
		//}

		//public IActionResult Index()
		//{
		//	return View(Carts);
		//}

		//public IActionResult AddToCart(int id, int SoLuong, string type = "Normal")
		//{
		//	var myCart = Carts;
		//	var item = myCart.SingleOrDefault(p => p.MaHh == id);

		//	if (item == null)//chưa có
		//	{
		//		var hangHoa = _context.Products.SingleOrDefault(p => p.ProductID == id);
		//		item = new Cart
		//		{
		//			MaHh = id,
		//			TenHH = hangHoa.ProductName,
		//			DonGia = hangHoa.OriginalPrice,
		//			SoLuong = SoLuong,
		//			Hinh = hangHoa.Image
		//		};
		//		myCart.Add(item);
		//	}
		//	else
		//	{
		//		item.SoLuong += SoLuong;
		//	}
		//	HttpContext.Session.Set("GioHang", myCart);

		//	if (type == "ajax")
		//	{
		//		return Json(new
		//		{
		//			SoLuong = Carts.Sum(c => c.SoLuong)
		//		});
		//	}
		//	return RedirectToAction("Index");
		//}
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
