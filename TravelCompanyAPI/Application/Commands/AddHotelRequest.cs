using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class AddHotelRequest: IRequest
{
    public AddHotelRequest(string hotelName, string categoryCode, string city, string country, string typeOfAccommodation, byte[] previewImageBytes)
    {
        HotelName = hotelName;
        CategoryCode = categoryCode;
        City = city;
        Country = country;
        PreviewImageBytes = previewImageBytes;
    }

    public string HotelName { get; set; }
    public string CategoryCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public byte[] PreviewImageBytes { get; set; }
}