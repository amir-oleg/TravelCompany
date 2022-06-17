namespace TravelCompanyAPI.Application.Responses;

public class GetHotelEavResponse
{
    public string HotelName { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int? PreviewImage { get; set; }
    public List<ServiceResponse> Services { get; set; } = new();
}