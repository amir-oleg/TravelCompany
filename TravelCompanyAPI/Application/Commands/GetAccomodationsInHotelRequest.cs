using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsInHotelRequest: IRequest<List<GetAccomodationsInHotelResponse>>
{
    public GetAccomodationsInHotelRequest(int hotelId)
    {
        HotelId = hotelId;
    }

    public int HotelId { get; set; }
}