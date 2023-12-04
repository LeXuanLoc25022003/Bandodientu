using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Bandodientu.Components
{
	[ViewComponent(Name = "ContactView")]
	public class ContactComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ContactComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofMenu = (from m in _context.Menus
							  where (m.IsActive == true) && (m.Position == 1)
							  select m).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));
		}
	}
}
