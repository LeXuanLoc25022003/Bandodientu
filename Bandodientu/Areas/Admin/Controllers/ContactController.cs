﻿using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ContactController : Controller
	{
		private readonly DataContext _context;
		public ContactController(DataContext context) 
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var mnList = _context.Contacts.OrderBy(m=>m.ContactID).ToList();
			return View(mnList);
		}
		public IActionResult Delete(int? id)
		{
			if( id== null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.Contacts.Find(id);
			if(mn == null)
			{
				return NotFound();
			}
			return View(mn);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var deleContact = _context.Contacts.Find(id);
			if(deleContact == null)
			{
				return NotFound();
			}
			_context.Contacts.Remove(deleContact);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Contacts.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Update(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }
    }
}