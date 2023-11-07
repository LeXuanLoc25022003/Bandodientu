using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var mnList = _context.Products.OrderBy(m=>m.ProductID).ToList();
            return View(mnList);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Products.Find(id);

            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Products.Find(id);
            if(deleMenu == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
