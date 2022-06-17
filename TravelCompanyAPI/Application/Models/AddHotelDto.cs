namespace TravelCompanyAPI.Application.Models;

public class AddHotelDto
{
    public string HotelName { get; set; }
    public string CategoreCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string TypeOfAccommodation { get; set; }
    public string PreviewImageBase64 { get; set; }
}