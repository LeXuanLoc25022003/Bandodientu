using Bandodientu.Areas.Admin.Models;

namespace Bandodientu.Models.ViewModels
{
	public class AdminUserListViewModel
	{
        public IEnumerable<AdminUser> AdminUsers { get; set; } = Enumerable.Empty<AdminUser>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
