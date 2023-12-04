using Bandodientu.Infrastructure;
using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
			if(!Function.IsLoginCustomer())
				return RedirectToAction("Dangnhap", "Home");
			return View();
		}
    }
}
