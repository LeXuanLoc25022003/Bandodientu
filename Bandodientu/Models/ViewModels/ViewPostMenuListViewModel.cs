namespace Bandodientu.Models.ViewModels
{
	public class ViewPostMenuListViewModel
	{
		public IEnumerable<view_Post_Menu> View_Post_Menus { get; set; } = Enumerable.Empty<view_Post_Menu>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
