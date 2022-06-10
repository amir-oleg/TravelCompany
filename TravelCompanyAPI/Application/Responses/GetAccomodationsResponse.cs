using TravelCompanyAPI.Application.Models;

namespace TravelCompanyAPI.Application.Responses;

public class GetAccomodationsResponse
{
    public ICollection<HotelDto> Hotels { get; set; } = new List<HotelDto>();
}