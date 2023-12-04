using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "UserView")]
	public class UserComponent : ViewComponent
	{
		private DataContext _context;
		public UserComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofMenu = (from m in _context.User
							  select m).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));
		}
	}
}
