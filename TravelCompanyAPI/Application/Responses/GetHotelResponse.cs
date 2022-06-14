namespace TravelCompanyAPI.Application.Responses;

public class GetHotelResponse
{
    public string HotelName { get; set; }
    public int CountOfStars { get; set; }
    public int? PreviewImage { get; set; }
    public string TypeOfDiet { get; set; }
    public string DateOfFoundation { get; set; }
    public int? MetersToBeach { get; set; }
}