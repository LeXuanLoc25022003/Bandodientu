using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "RelatedProducts")]
	public class RelatedProductsComponent : ViewComponent
	{
		private readonly DataContext _context;
		public RelatedProductsComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where(p.IsActive==true)
								 select p).Take(8).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
