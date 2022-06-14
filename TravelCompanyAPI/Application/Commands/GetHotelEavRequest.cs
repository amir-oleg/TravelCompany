using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelEavRequest: IRequest<GetHotelEavResponse>
{
    public GetHotelEavRequest(int hotelId)
    {
        HotelId = hotelId;
    }

    public int HotelId { get; set; }
}