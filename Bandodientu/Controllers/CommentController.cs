using Bandodientu.Models;
using Bandodientu.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Bandodientu.Controllers
{
	public class CommentController : Controller
	{
		private readonly DataContext _context;
		int PageSize = 2;
		public CommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index(int productPage=1)
		{
            return View(
				new CommentListViewModel
				{
				Comments = _context.Comments
				.Skip((productPage - 1) * PageSize)
				.Take(PageSize),
				PagingInfo = new PagingInfo
					{
					ItemsPerPage = PageSize,
					CurrentPage = productPage,
					TotalItems = _context.Comments.Count()
					}
				}
			);
        }
		[HttpPost]
		public bool Create(string name, string email, string review, int id, int rate)
		{
			try
			{
				Comment comment = new Comment();
				comment.YourName = name;
				comment.YourEmail = email;
				comment.Review = review;
				comment.ProductID = id;
				comment.Rate = rate;
				comment.DateTime = DateTime.Now;
				comment.IsActive = true;
				_context.Add(comment);
				_context.SaveChangesAsync();

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
