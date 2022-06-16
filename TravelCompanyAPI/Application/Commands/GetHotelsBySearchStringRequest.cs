using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelsBySearchStringRequest: IRequest<GetHotelsBySearchStringResponse>
{
    public GetHotelsBySearchStringRequest(string searchString, int page)
    {
        SearchString = searchString.ToLower();
        Page = page;
    }

    public string SearchString { get; set; }
    public int Page { get; set; }
}