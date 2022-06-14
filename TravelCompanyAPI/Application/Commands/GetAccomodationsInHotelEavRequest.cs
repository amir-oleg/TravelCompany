using MediatR;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsInHotelEavRequest : IRequest<List<GetAccomodationsInHotelEavResponse>>
{
    public GetAccomodationsInHotelEavRequest(int hotelId, DateTime startDate, DateTime endDate, int guests)
    {
        HotelId = hotelId;
        StartDate = startDate;
        EndDate = endDate;
        Guests = guests;
    }

    public int HotelId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Guests { get; set; }
}