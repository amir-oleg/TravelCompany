using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsRequest: IRequest<GetAccomodationsResponse>
{
    public GetAccomodationsRequest(string country, DateTime startDate, DateTime endDate, int guests)
    {
        Country = country;
        StartDate = startDate;
        EndDate = endDate;
        Guests = guests;
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Guests { get; set; }
    public string Country { get; set; }
}