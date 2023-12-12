using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Areas.Admin.Components
{
    [ViewComponent(Name = "Contact")]
    public class ContactComponet : ViewComponent
    {
        private readonly DataContext _context;
        public ContactComponet(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.Contacts
                          where (mn.IsActive == true)
                          select mn).Take(4).ToList();
            ViewBag.comment = _context.Contacts.Count(m => m.IsActive == true).ToString();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
