namespace Bandodientu.Models.ViewModels
{
	public class PagingInfo
	{
		public int TotalItems { get; set; } //tổng sản phẩm
		public int ItemsPerPage { get; set; } //có bao nheieu sản phẩm trên 1 trang
		public int CurrentPage { get; set; } //trang hiện tại
		public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
	}
}
