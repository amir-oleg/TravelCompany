using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class AddTourRequest: IRequest
{
    public AddTourRequest(string name, int startCityId, int endCityId, string transportType, int guestsCount, int childrenCount, int days, string dietTypeCode, string[] tourCategoryCodes, decimal price, ServiceResponse[] attributtes, int accomodationId, string image)
    {
        Name = name;
        StartCityId = startCityId;
        EndCityId = endCityId;
        TransportType = transportType;
        GuestsCount = guestsCount;
        ChildrenCount = childrenCount;
        Days = days;
        DietTypeCode = dietTypeCode;
        TourCategoryCodes = tourCategoryCodes ?? Array.Empty<string>();
        Price = price;
        Attributtes = attributtes ?? Array.Empty<ServiceResponse>();
        AccomodationId = accomodationId;
        Image = image;
    }

    public string Name { get; set; }
    public int StartCityId { get; set; }
    public int EndCityId { get; set; }
    public string TransportType { get; set; }
    public int GuestsCount { get; set; }
    public int ChildrenCount { get; set; }
    public int Days { get; set; }
    public string DietTypeCode { get; set; }
    public string[] TourCategoryCodes { get; set; }
    public decimal Price { get; set; }
    public ServiceResponse[] Attributtes { get; set; }
    public int AccomodationId { get; set; }
    public string Image { get; set; }
}