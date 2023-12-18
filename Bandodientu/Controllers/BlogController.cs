using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Bandodientu.Controllers
{
	public class BlogController : Controller
	{
		int PageSize = 6;
		private readonly DataContext _context;

		public BlogController(DataContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(int productPage = 1)
			{
				return View(
					new ViewPostMenuListViewModel
					{
						View_Post_Menus = _context.view_Post_Menus
						.Skip((productPage - 1) * PageSize)
						.Take(PageSize),
						PagingInfo = new PagingInfo
						{
							ItemsPerPage = PageSize,
							CurrentPage = productPage,
							TotalItems = _context.view_Post_Menus.Count()
						}
					}
					);
			}
		}
	}