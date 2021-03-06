using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetManagersStatisticsRequest: IRequest<IEnumerable<GetManagersStatisticsResponse>>
{
    public GetManagersStatisticsRequest(string month)
    {
        Month = month;
    }
    public string Month { get; set; }
}