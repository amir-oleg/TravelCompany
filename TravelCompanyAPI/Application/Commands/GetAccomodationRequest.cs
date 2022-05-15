using MediatR;

namespace TravelCompanyAPI.Application.Commands
{
    public class GetAccomodationRequest: IRequest<GetAccomodationInHotelResponse>
    {
        public GetAccomodationRequest(int hotelId, int accomodationId)
        {
            HotelId = hotelId;
            AccomodationId = accomodationId;
        }

        public int HotelId { get; set; }
        public int AccomodationId { get; set; }
    }
}
