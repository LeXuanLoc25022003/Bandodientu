using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "Product")]
	public class ProductComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ProductComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where(p.IsActive==true)
								 select p).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
