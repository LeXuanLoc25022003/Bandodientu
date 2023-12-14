using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "OrderDetail")]
	public class OrderDetailComponent : ViewComponent
	{
		private readonly DataContext _context;

		public OrderDetailComponent(DataContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofComment = (from order in _context.Orders
								 select order).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofComment));
		}
	}
}
