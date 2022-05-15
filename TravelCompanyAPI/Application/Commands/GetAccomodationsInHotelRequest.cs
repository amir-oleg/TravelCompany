using MediatR;

namespace TravelCompanyAPI.Application.Commands
{
    internal class GetAccomodationsInHotelRequest: IRequest<List<GetAccomodationInHotelResponse>>
    {
        public GetAccomodationsInHotelRequest(int hotelId)
        {
            HotelId = hotelId;
        }

        public int HotelId { get; set; }
    }
}
