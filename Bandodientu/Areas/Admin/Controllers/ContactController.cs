using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Drawing.Printing;

namespace Bandodientu.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ContactController : Controller
	{
		private readonly DataContext _context;
        int PageSize = 5;
		public ContactController(DataContext context) 
		{
			_context = context;
		}
		public IActionResult Index(int productPage = 1)
		{
            return View(
            new ContactListViewModel
            {
                Contacts = _context.Contacts
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = _context.Contacts.Count()
                }
            }
            );
        }
        public async Task<IActionResult> Search(string keywords,int month,int year, int productPage = 1)
        {
            return View("Index",
                new ContactListViewModel
                {
                    Contacts = _context.Contacts
                    .Where(m => m.Name.Contains(keywords) || m.CreateDate.Month == month || m.CreateDate.Year == year)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Contacts.Count()
                    }
                }
                );
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
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
