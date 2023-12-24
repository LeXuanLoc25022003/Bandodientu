using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Bandodientu.Utilities;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly DataContext _context;
        public ProfileController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
		//public bool Edit(int id, string fullName, string about, string company, string Job, string Country, string Address, string Phone, string Email,string password)
		//{
		//    try
		//    {
		//        var mn = _context.AdminUsers.Find(id);
		//        if (mn == null)
		//        {
		//            return false;
		//        }
		//        AdminUser comment = new AdminUser();
		//        comment.UserID = id;
		//        comment.UserName = fullName;
		//        comment.About = about;
		//        comment.Company = company;
		//        comment.Job = Job;
		//        comment.Country = Country;
		//        comment.Address = Address;
		//        comment.Phone = Phone;
		//        comment.Email = Email;
		//        comment.Password = password;
		//        _context.AdminUsers.Update(comment);
		//        _context.SaveChanges();

		//        return true;
		//    }
		//    catch
		//    {
		//        return false;
		//    }
		//}
		public IActionResult Edit()
		{

			var mn = _context.AdminUsers.Where(m=>m.UserID==Function._UserID).ToList();
			return View(mn);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit(AdminUser user)
		{
			user.UserID=Function._UserID;
			user.IsActive = true;
			user.Password=Function._Password;
			_context.AdminUsers.Update(user);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
