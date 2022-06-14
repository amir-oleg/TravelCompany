using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelHandler:IRequestHandler<GetHotelRequest, GetHotelResponse>
{
    private readonly TravelCompanyClassicContext _context;

    public GetHotelHandler(TravelCompanyClassicContext context)
    {
        _context = context;
    }

    public async Task<GetHotelResponse> Handle(GetHotelRequest request, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId, cancellationToken);

        return new GetHotelResponse
        {
            CountOfStars = hotel.CountOfStars,
            HotelName = hotel.Name,
            PreviewImage = hotel.PreviewImageId,
            DateOfFoundation = hotel.DateOfFoundation.HasValue ? hotel.DateOfFoundation.Value.ToString("dd-MM-yyyy") : "",
            MetersToBeach = hotel.MetrsToBeach,
            TypeOfDiet = hotel.TypeOfDiet
        };
    }
}