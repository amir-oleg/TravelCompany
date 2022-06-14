namespace TravelCompanyAPI.Application.Responses;

public class GetHotelEavResponse
{
    public string HotelName { get; set; }
    public int CountOfStars { get; set; }
    public int? PreviewImage { get; set; }
    public List<ServiceResponse> Services { get; set; } = new();
}