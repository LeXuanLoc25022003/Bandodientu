namespace Bandodientu.Models.ViewModels
{
	public class MenuListViewModel
	{
        public IEnumerable<Menu> Menus { get; set; } = Enumerable.Empty<Menu>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
