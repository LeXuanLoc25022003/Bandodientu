using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        int PageSize = 5;
        public readonly DataContext _context;
        public AdminUserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(int productPage = 1)
        {
            return View(
            new AdminUserListViewModel
            {
                AdminUsers = _context.AdminUsers
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.AdminUsers.Count()
                }
            }
            );
        }
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            return View("Index",
                new AdminUserListViewModel
                {
                    AdminUsers = _context.AdminUsers
                    .Where(m => m.UserName.Equals(keywords))
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.AdminUsers.Count()
                    }
                }
                );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminUsers == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.AdminUsers
                //.Include(t => t.PostOrder)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }
        public IActionResult Delete(int? id)
        {
            if(id==null && id == 0)
            {
                return NotFound();
            }
            var mn = _context.AdminUsers.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var mnList = _context.AdminUsers.Find(id);
            if(mnList == null)
            {
                return NotFound();
            }
            _context.AdminUsers.Remove(mnList);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.AdminUsers.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(AdminUser comment)
        {
            if (ModelState.IsValid)
            {
                _context.AdminUsers.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
    }
}
