using MediatR;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class AddHotelRequest: IRequest
{
    public AddHotelRequest(string hotelName, string categoryCode, int city, string previewImageBytes, ServiceResponse[] hotelAttributes, AddAccomodation[] accomodations)
    {
        HotelName = hotelName;
        CategoryCode = categoryCode;
        City = city;
        PreviewImageBytes = Convert.FromBase64String(previewImageBytes);
        HotelAttributes = hotelAttributes;
        Accomodations = accomodations.Select(acc => new AddAccomodationRequest
        {
            Name = acc.Name,
            Capacity = acc.Capacity,
            PricePerDay = acc.PricePerDay,
            Images = acc.Images.Select(Convert.FromBase64String).ToList(),
            Attributes = acc.Attributes
        }).ToArray();
    }

    public string HotelName { get; set; }
    public string CategoryCode { get; set; }
    public int City { get; set; }
    public byte[] PreviewImageBytes { get; set; }
    public ServiceResponse[] HotelAttributes { get; set; }
    public AddAccomodationRequest[] Accomodations { get; set; }
}

public class AddAccomodationRequest
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
    public List<byte[]> Images { get; set; }
    public ServiceResponse[] Attributes { get; set; }
}