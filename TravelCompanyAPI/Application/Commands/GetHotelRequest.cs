using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelRequest:IRequest<GetHotelResponse>
{
    public GetHotelRequest(int hotelId)
    {
        HotelId = hotelId;
    }

    public int HotelId { get; set; }
}