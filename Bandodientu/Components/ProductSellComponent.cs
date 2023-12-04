using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "ProductSell")]
	public class ProductSellComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ProductSellComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where (p.IsActive == true)
								 orderby p.ProductID descending
								 select p).Take(6).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
