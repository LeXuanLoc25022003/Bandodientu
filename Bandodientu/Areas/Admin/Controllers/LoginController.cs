using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class LoginController : Controller
	{
		private readonly DataContext _context;
		public LoginController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(AdminUser user)
		{
			if (user == null)
			{
				return NotFound();
			}

			string pw = Function.MD5Password(user.Password);

			var check = _context.AdminUsers.Where(m=>(m.Email==user.Email) && (m.Password==pw)).FirstOrDefault();
			if (check == null)
			{
				Function._Message = "Sai tài khoản hoặc mặt khẩu";
				return RedirectToAction("Index", "Login");
			}
			Function._Message = string.Empty;
			Function._UserID = check.UserID;
			Function._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
			Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            Function._About = string.IsNullOrEmpty(check.About) ? string.Empty : check.About;
            Function._Image = string.IsNullOrEmpty(check.Image) ? string.Empty : check.Image;
            Function._Company = string.IsNullOrEmpty(check.Company) ? string.Empty : check.Company;
            Function._Job = string.IsNullOrEmpty(check.Job) ? string.Empty : check.Job;
            Function._Country = string.IsNullOrEmpty(check.Country) ? string.Empty : check.Country;
            Function._Address = string.IsNullOrEmpty(check.Address) ? string.Empty : check.Address;
            Function.Phone = string.IsNullOrEmpty(check.Phone) ? string.Empty : check.Phone;
            Function.Image = string.IsNullOrEmpty(check.Image) ? string.Empty : check.Image;
            Function._Password = string.IsNullOrEmpty(check.Password) ? string.Empty : check.Password;
            return RedirectToAction("Index","Home");
		}
	}
}
