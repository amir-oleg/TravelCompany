using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetFreeOrdersRequest: IRequest<GetFreeOrdersResponse>
{
}