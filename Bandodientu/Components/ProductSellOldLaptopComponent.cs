using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bandodientu.Components
{
	[ViewComponent(Name = "ProductSellOldLaptop")]
	public class ProductSellOldLaptopComponent : ViewComponent
	{
		private readonly DataContext _context;
		public ProductSellOldLaptopComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofProduct = (from p in _context.Products
								 where (p.IsActive == true && p.MenuID==5)
								 select p).Take(4).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofProduct));
		}
	}
}
