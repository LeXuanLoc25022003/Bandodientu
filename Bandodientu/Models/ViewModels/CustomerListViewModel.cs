namespace Bandodientu.Models.ViewModels
{
	public class CustomerListViewModel
	{
        public IEnumerable<Customer> Customers { get; set; } = Enumerable.Empty<Customer>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
