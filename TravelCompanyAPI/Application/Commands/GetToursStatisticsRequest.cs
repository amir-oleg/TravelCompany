using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetToursStatisticsRequest: IRequest<IEnumerable<GetToursStatisticsResponse>>
{
    public GetToursStatisticsRequest(string month)
    {
        Month = month;
    }
    public string Month { get; set; }
}