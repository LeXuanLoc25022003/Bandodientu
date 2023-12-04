using Bandodientu.Infrastructure;
using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	public class YourCart : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View(HttpContext.Session.GetJson<Cart>("cart"));
		}
	}
}
