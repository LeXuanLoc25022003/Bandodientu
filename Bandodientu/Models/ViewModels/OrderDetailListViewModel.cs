namespace Bandodientu.Models.ViewModels
{
	public class OrderDetailListViewModel
	{
        public IEnumerable<OrderDetail> OrderDetails { get; set; } = Enumerable.Empty<OrderDetail>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
