﻿using Bandodientu.Areas.Admin.Models;
using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bandodientu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.customers.OrderBy(m=>m.CustomerID).ToList();
            return View(mnList);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null && id == 0)
            {
                return NotFound();
            }
            var mn = _context.customers.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.customers.Find(id);
            if(deleMenu == null)
            {
                return NotFound();
            }
            _context.customers.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
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
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.customers.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Customer comment)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
    }
}