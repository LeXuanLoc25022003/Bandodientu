using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Bandodientu.Controllers
{
	public class OrderDetailController : Controller
	{
		private readonly DataContext _context;

		public OrderDetailController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index(int id)
		{
			var items = _context.OrderDetails.Where(m => m.OrderID == id).ToList();
			ViewBag.Product = _context.Products.Where(p => p.IsActive == true).ToList();
			return View(items);
		}
	}
}