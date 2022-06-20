using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyAPI.Helpers;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelsBySearchStringHandler: IRequestHandler<GetHotelsBySearchStringRequest, GetHotelsBySearchStringResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetHotelsBySearchStringHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetHotelsBySearchStringResponse> Handle(GetHotelsBySearchStringRequest request, CancellationToken cancellationToken)
    {
        var thisMany = (request.Page - 1) * GeneralConsts.PageSize;

        var hotels = _context.Hotels
            .Include(hotel => hotel.City)
            .ThenInclude(city => city.Country)
            .Where(hotel => hotel.Name.ToLower() == request.SearchString ||
                            hotel.City.Name.ToLower() == request.SearchString ||
                            hotel.City.Country.Name.ToLower() == request.SearchString);

        double count = await hotels.CountAsync(cancellationToken);

        var results = await hotels.Skip(thisMany)
            .Take(GeneralConsts.PageSize)
            .AsNoTracking()
            .Select(hotel => new
            {
                hotel.Id,
                hotel.Name,
                hotel.PreviewImageId
            })
            .ToListAsync(cancellationToken);

        return new GetHotelsBySearchStringResponse
        {
            Hotels = hotels.Select(hotel => new HotelsBySearchString()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                PreviewImage = hotel.PreviewImageId
            }).ToList(),
            PageCount = (int)Math.Ceiling(count / GeneralConsts.PageSize)
        };
    }
}