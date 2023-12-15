using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Bandodientu.Controllers
{
	public class HistoryOrderController : Controller
	{
		private readonly DataContext _context;

		public HistoryOrderController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var customer = _context.customers.FirstOrDefault(c => (c.CustomerID == Function._CustomerID));
			ViewBag.Status = _context.OrderStatuses.ToList();
			var items = _context.Orders.Where(o => (o.CustomerID == customer.CustomerID)).ToList();
			return View(items);
		}
	}
}