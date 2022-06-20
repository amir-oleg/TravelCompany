namespace TravelCompanyAPI.Application.Models;

public class SearchToursByWayDto
{
    public string StartPlace { get; set; }
    public string EndPlace { get; set; }
    public string[] TransportType { get; set; }
    public int GuestsCount { get; set; }
    public int ChildrenCount { get; set; }
    public DateTime StartDate { get; set; }
    public int Days { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string> HotelCategories { get; set; }
    public List<string> TourCategories { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
}