using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bandodientu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

		public HomeController(ILogger<HomeController> logger,DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
			return View();
        }
        public ActionResult Dangky()
        {
            var mnList = (from m in _context.Locations
                          select new SelectListItem()
                          {
                              Text = m.Name,
                              Value = m.LocationID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "---Select---",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(Customer mn)
        {
			if (mn == null)
			{
				return NotFound();
			}
			var check = _context.customers.Where(m => m.Email == mn.Email).FirstOrDefault();
			if (check != null)
			{
				Function._MessageEmail = "Duplicate Email!";
				return RedirectToAction("Dangky", "Home");
			}
			Function._MessageEmail = string.Empty;
			mn.Password = Function.MD5Password(mn.Password);
			_context.Add(mn);
			_context.SaveChanges();
			return RedirectToAction("Dangnhap", "Home");
		}
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(Customer user)
        {
			if (user == null)
			{
				return NotFound();
			}

			string pw = Function.MD5Password(user.Password);

			var check = _context.customers.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
			if (check == null)
			{
				Function._Message = "Sai tài khoản hoặc mặt khẩu";
				return RedirectToAction("Index", "Login");
			}
			Function._Message = string.Empty;
			Function._CustomerID = check.CustomerID;
			Function._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
			Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
			Function._Phone = string.IsNullOrEmpty(check.Phone) ? string.Empty : check.Phone;
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Privacy()
        {
            return View();
        }
        [Route("/post-{slug}-{id:long}.html",Name = "Details")]

        public IActionResult Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var post = _context.Products
                .FirstOrDefault(m => (m.ProductID == id));
            var comments = _context.Comments
                .Count(m => (m.ProductID == id));
			float rate1 = _context.Comments
	        .Count(m => (m.Rate == 1));
			float rate2 = _context.Comments
                .Count(m => (m.Rate == 2));
			float rate3 = _context.Comments
                .Count(m => (m.Rate == 3));
			float rate4 = _context.Comments
                .Count(m => (m.Rate == 4));
			float rate5 = _context.Comments
                .Count(m => (m.Rate == 5));
			float rate = (rate1 + rate2 + rate3 + rate4 + rate5);
			float percent1 = ((float)rate1 / (float)rate) * 100;
			float percent2 = ((float)rate2 / (float)rate) * 100;
			float percent3 = ((float)rate3 / (float)rate) * 100;
			float percent4 = ((float)rate4 / (float)rate) * 100;
			float percent5 = ((float)rate5 / (float)rate) * 100;
			if (post == null)
            {
                return NotFound();
            }
			ViewBag.ProductID = post.ProductID;
            ViewBag.comment = comments.ToString();
			ViewBag.rate1 = rate1.ToString();
			ViewBag.rate2 = rate2.ToString();
			ViewBag.rate3 = rate3.ToString();
			ViewBag.rate4 = rate4.ToString();
			ViewBag.rate5 = rate5.ToString();
			ViewBag.rate = rate.ToString();
			ViewBag.percent1 = (percent1);
			ViewBag.percent2 = (percent2);
			ViewBag.percent3 = (percent3);
			ViewBag.percent4 = (percent4);
			ViewBag.percent5 = (percent5);
			return View(post);
            
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
		[HttpPost]
		public JsonResult CheckLogin()
		{
			bool isLogin = Function.IsLoginCustomer();
			return Json(new { IsLogin = isLogin, Message = isLogin ? null : "Đăng nhập để gửi phản hồi !" });
		}
	}
}