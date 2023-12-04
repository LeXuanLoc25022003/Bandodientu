using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (!Function.IsLogin())
				return RedirectToAction("Index", "Login");
			return View();
		}
		public IActionResult LoginOut()
		{
			Function._UserID = 0;
			Function._UserName = string.Empty;
			Function._Email = string.Empty;
			Function._Message = string.Empty;
			Function._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
	}
}
