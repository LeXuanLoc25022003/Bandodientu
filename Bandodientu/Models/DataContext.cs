﻿using Bandodientu.Models;
using Bandodientu.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Customer> User { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
		public DbSet<Comment> Comments { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Customer> customers { get; set; }
		public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> postComments { get; set; }
        public DbSet<ReplyComment> replyComments { get; set; }

	}
}
