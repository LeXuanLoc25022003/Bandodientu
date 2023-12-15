using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "ProductSellLaptop")]
	public class ProductSellLaptopComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ProductSellLaptopComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where (p.IsActive == true && p.MenuID==4)
								 select p).Take(8).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
