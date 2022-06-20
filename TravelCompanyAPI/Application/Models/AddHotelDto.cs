using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Models;

public class AddHotelDto
{
    public string HotelName { get; set; }
    public string CategoryCode { get; set; }
    public int City { get; set; }
    public string PreviewImageBase64 { get; set; }
    public ServiceResponse[] HotelAttributes { get; set; }
    public AddAccomodation[] Accomodations { get; set; }
}

public class AddAccomodation
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
    public int Count { get; set; }
    public string[] Images { get; set; }
    public ServiceResponse[] Attributes { get; set; }
}