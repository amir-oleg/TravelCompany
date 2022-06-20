using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsHandler: IRequestHandler<GetAccomodationsRequest, GetAccomodationsResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetAccomodationsHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetAccomodationsResponse> Handle(GetAccomodationsRequest request, CancellationToken cancellationToken)
    {
        var hotels = await _context.Hotels
            .Include(h => h.AccomodationTypes)
            .ThenInclude(act => act.Accomodations)
            .ThenInclude(a => a.Occupancies)
            .Include(h => h.City)
            .ThenInclude(c => c.Country)
            .Include(h => h.CategoryCodeNavigation)
            .Where(h => h.City.Country.Name.ToLower() == request.Country.ToLower() &&
                h.AccomodationTypes.Any(a => a.Capacity == request.Guests))
            .ToListAsync(cancellationToken);

        hotels = hotels.Where(h => h.AccomodationTypes.Any(act => act.Accomodations.Any(acc =>
            acc.Occupancies.All(occ =>
                occ.AccomodationId == acc.Id &&
                !(occ.StartDate >= request.StartDate && occ.StartDate < request.EndDate) &&
                !(occ.EndDate >= request.StartDate && occ.EndDate < request.EndDate))))).ToList();

        var result = new GetAccomodationsResponse();

        var days = (request.EndDate - request.StartDate).Days;

        foreach (var hotel in hotels)
        {
            result.Hotels.Add(new HotelDto()
            {
                Id = hotel.Id,
                Category = hotel.CategoryCodeNavigation.Value,
                City = hotel.City.Name,
                Country = hotel.City.Country.Name,
                LowestPrice = await _context.AccomodationTypes.Where(a => a.HotelId == hotel.Id)
                                                          .Select(a => a.PricePerDay)
                                                          .MinAsync(cancellationToken) * days,
                Name = hotel.Name,
                PreviewImage = hotel.PreviewImageId
            });
        }
        return result;
    }
}