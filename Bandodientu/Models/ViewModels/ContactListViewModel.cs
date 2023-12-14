namespace Bandodientu.Models.ViewModels
{
	public class ContactListViewModel
	{
        public IEnumerable<Contact> Contacts { get; set; } = Enumerable.Empty<Contact>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
