﻿using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        public readonly DataContext _context;
        public AdminUserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.AdminUsers.OrderBy(m=>m.UserID).ToList();
            return View(mnList);
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
