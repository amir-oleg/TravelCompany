using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetPersonalDataRequest: IRequest<GetPersonalResponse>
{
    public GetPersonalDataRequest(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}