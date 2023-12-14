using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace Bandodientu.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public int PageSize = 6;
        public ProductController(DataContext context)
        {
            _context = context;
		}
		public class Price
		{
			public decimal Min {  get; set; }
			public decimal Max { get; set; }
		}

		[HttpPost]
		public IActionResult GetFilteredProducts([FromBody] FilterData filter)
		{
			var filterproduct = _context.Products.ToList();

			if (filter.Price != null && filter.Price.Count > 0 && !filter.Price.Contains("all"))
			{
				List<Price> prices = new List<Price>();

				foreach (var range in filter.Price)
				{
					var value = range.Split("-").ToArray();
					Price price = new Price();

					if (decimal.TryParse(value[0], out decimal min) && decimal.TryParse(value[1], out decimal max))
					{
						price.Min = min;
						price.Max = max;
						prices.Add(price);
					}
				}

				filterproduct = filterproduct
					.Where(p => prices.Any(r => p.DiscountedPrice >= r.Min && p.DiscountedPrice <= r.Max))
					.ToList();
			}
			if (filter.MenuID != null && filter.MenuID.Count > 0)
			{
				filterproduct = filterproduct.Where(p => filter.MenuID.Contains(p.MenuID)).ToList();
			}
			if (filter.Color != null && filter.Color.Count > 0 && !filter.Color.Contains("all"))
			{
				filterproduct = filterproduct.Where(p => filter.Color.Contains(p.Color)).ToList();
			}

			return PartialView("_ReturnProduct", filterproduct);
		}

		public async Task<IActionResult> Index(int productPage = 1)
        {
			var cphone = _context.Products.Count(m => m.MenuID==3);
			ViewBag.cphone = cphone.ToString();
			var claptop = _context.Products.Count(m => m.MenuID == 4);
			ViewBag.claptop = claptop.ToString();
			var coldlaptop = _context.Products.Count(m => m.MenuID == 5);
			ViewBag.coldlaptop = coldlaptop.ToString();
			var phone = _context.Products.Count(m => m.OriginalPrice >= 1000000 && m.OriginalPrice <= 5000000);
            ViewBag.phone = phone.ToString();
			var laptop = _context.Products.Count(m => m.OriginalPrice >= 6000000 && m.OriginalPrice <= 10000000);
			ViewBag.laptop = laptop.ToString();
			var oldlaptop = _context.Products.Count(m => m.OriginalPrice >= 11000000 && m.OriginalPrice <= 15000000);
			ViewBag.oldlaptop = oldlaptop.ToString();
			var pricelaptop = _context.Products.Count(m => m.OriginalPrice >= 16000000 && m.OriginalPrice <= 20000000);
			ViewBag.pricelaptop = pricelaptop.ToString();
			var max = _context.Products.Count();
			ViewBag.max = max.ToString();
			var white = _context.Products.Count(m => m.Color == "Trắng");
			ViewBag.white = white.ToString();
			var black = _context.Products.Count(m => m.Color == "Đen");
			ViewBag.black = black.ToString();
			var green = _context.Products.Count(m => m.Color == "Xanh");
			ViewBag.green = green.ToString();
			var yellow = _context.Products.Count(m => m.Color == "Vàng");
			ViewBag.yellow = yellow.ToString();
			return View(
                new ProductListViewModel
                {
                    Products = _context.Products
                    .Skip((productPage - 1)*PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo 
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Products.Count()
                    }
                }
                );
        }
        [HttpPost]
		public async Task<IActionResult> Search(string keywords,int productPage = 1)
		{
			return View("Index",
				new ProductListViewModel
				{
					Products = _context.Products
                    .Where(m=>m.ProductName.Contains(keywords))
					.Skip((productPage - 1) * PageSize)
					.Take(PageSize),
					PagingInfo = new PagingInfo
					{
						ItemsPerPage = PageSize,
						CurrentPage = productPage,
						TotalItems = _context.Products.Count()
					}
				}
				);
		}
        public async Task<IActionResult> OrderBy(int productPage = 1)
        {
            return View("Index",
                new ProductListViewModel
                {
                    Products = _context.Products
                    .OrderBy(m=>m.DiscountedPrice).ToList()
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = productPage,
                        TotalItems = _context.Products.Count()
                    }
                }
                );
        }
    }
}
