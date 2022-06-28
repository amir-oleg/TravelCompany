using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelStatsRequest: IRequest<GetHotelStatsResponse>
{
    public GetHotelStatsRequest(int hotelId)
    {
        HotelId = hotelId;
    }

    public int HotelId { get; set; }
}