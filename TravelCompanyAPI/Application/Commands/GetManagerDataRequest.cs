using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetManagerDataRequest: IRequest<GetManagerDataResponse>
{
    public GetManagerDataRequest(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}