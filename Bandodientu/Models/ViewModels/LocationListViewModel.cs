namespace Bandodientu.Models.ViewModels
{
	public class LocationListViewModel
	{
        public IEnumerable<Location> Locations { get; set; } = Enumerable.Empty<Location>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
