namespace TravelCompanyAPI.Application.Responses;

public class GetTourByIdResponse
{
    public string TourName { get; set; }
    public string StartCountry { get; set; }
    public string EndCountry { get; set; }
    public string StartCity { get; set; }
    public string EndCity { get; set; }
    public decimal Price { get; set; }
    public string DietType { get; set; }
    public List<string> TourCategories { get; set; }
    public int Guests { get; set; }
    public int ChildrenCount { get; set; }
    public int Days { get; set; }
    public List<ServiceResponse> Services { get; set; }
    public int PreviewImageId { get; set; }
    public HotelWithAccResponse Accomodation { get; set; }
}

public class HotelWithAccResponse
{
    public int AccomodationId { get; set; }
    public string HotelName { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int HotelPreviewImageId { get; set; }
    public List<ServiceResponse> HotelServices { get; set; } = new();
    public string AccomodationName { get; set; }
    public int Capacity { get; set; }
    public List<ServiceResponse> AccomodationServices { get; set; } = new();
    public IEnumerable<int> AccomodationImages { get; set; }
}