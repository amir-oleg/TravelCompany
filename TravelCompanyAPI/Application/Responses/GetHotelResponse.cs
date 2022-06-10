namespace TravelCompanyAPI.Application.Responses;

public class GetHotelResponse
{
    public string HotelName { get; set; }
    public int CountOfStars { get; set; }
    public IEnumerable<string> Services { get; set; }
    public int? PreviewImage { get; set; }
}