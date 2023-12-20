using Bandodientu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Bandodientu.Controllers
{
	public class ReplyCommentController : Controller
	{
		private readonly DataContext _context;
		public ReplyCommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public bool Create(int customerID, int commentID, string messeage,int postID)
		{
			try
			{
				ReplyComment c = new ReplyComment();
				c.CustomerID = customerID;
				c.CommentID = commentID;
				c.PostID= postID;
				c.Messeage = messeage;
				c.CreateDate = DateTime.Now;
				c.IsActive = true;
				_context.Add(c);
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
