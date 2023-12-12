using Bandodientu.Models;
using Bandodientu.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Bandodientu.Controllers
{
	public class PostController : Controller
	{
		private readonly ILogger<PostController> _logger;
		private readonly DataContext _context;

		public PostController(ILogger<PostController> logger, DataContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public bool Create(string name, string email, string comment, int id)
		{
			try
			{
				PostComment c = new PostComment();
				c.Name = name;
				c.Email = email;
				c.Message = comment;
				c.PostID = id;
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
		public IActionResult Privacy()
		{
			return View();
		}
		[Route("/list-{slug}-{id:long}.html", Name = "PostDetails")]

		public IActionResult PostDetails(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _context.Posts
				.FirstOrDefault(m => m.PostID == id);
			var comments = _context.postComments
				.Count(m => (m.PostID == id));
			if (post == null)
			{
				return NotFound();
			}
			ViewBag.PostID = post.PostID;
			ViewBag.comment = comments.ToString();
			return View(post);

		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}