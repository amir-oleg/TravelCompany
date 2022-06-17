using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelEavHandler: IRequestHandler<GetHotelEavRequest, GetHotelEavResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetHotelEavHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetHotelEavResponse> Handle(GetHotelEavRequest request, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels
            .Include(hotel => hotel.CategoryCodeNavigation)
            .Include(hotel => hotel.City)
                .ThenInclude(city => city.Country)
            .Include(hotel => hotel.ValuesHotelsAttributes)
                .ThenInclude(vha => vha.HotelAttribute)
            .FirstOrDefaultAsync(h => h.Id == request.HotelId, cancellationToken);

        var response = new GetHotelEavResponse
        {
            Category = hotel.CategoryCodeNavigation.Value,
            City = hotel.City.Name,
            Country = hotel.City.Country.Name,
            HotelName = hotel.Name,
            PreviewImage = hotel.PreviewImageId
        };

        foreach (var hotelValuesHotelsAttribute in hotel.ValuesHotelsAttributes)
        {
            response.Services.Add(new ServiceResponse()
            {
                Name = hotelValuesHotelsAttribute.HotelAttribute.Name,
                MeasureOfUnit = hotelValuesHotelsAttribute.HotelAttribute.MeasureUnit,
                Value = hotelValuesHotelsAttribute.Value
            });
        }

        return response;
    }
}