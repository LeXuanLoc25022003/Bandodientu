using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Controllers
{
	public class CommentController : Controller
	{
		private readonly DataContext _context;
		public CommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
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
