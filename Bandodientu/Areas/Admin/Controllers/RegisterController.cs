using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DataContext _context;
        public RegisterController(DataContext context)
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
            if(user == null)
            {
                return NotFound();
            }
            var check = _context.AdminUsers.Where(m=>m.Email == user.Email).FirstOrDefault();
            if(check != null)
            {
                Function._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }
            Function._MessageEmail = string.Empty;
            user.Password=Function.MD5Password(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
