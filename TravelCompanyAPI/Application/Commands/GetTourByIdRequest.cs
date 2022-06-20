using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetTourByIdRequest: IRequest<GetTourByIdResponse>
{
    public GetTourByIdRequest(int tourId)
    {
        TourId = tourId;
    }

    public int TourId { get; set; }
}