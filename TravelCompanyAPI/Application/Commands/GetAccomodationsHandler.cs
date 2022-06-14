using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsHandler: IRequestHandler<GetAccomodationsRequest, GetAccomodationsResponse>
{
    private readonly TravelCompanyClassicContext _context;

    public GetAccomodationsHandler(TravelCompanyClassicContext context)
    {
        _context = context;
    }

    public async Task<GetAccomodationsResponse> Handle(GetAccomodationsRequest request, CancellationToken cancellationToken)
    {
        var hotels = await _context.Hotels.Include(h => h.Accomodations)
            .ThenInclude(a => a.Occupancies)
            .Include(h => h.City)
            .ThenInclude(c => c.Country)
            .Where(h => h.City.Country.Name.ToLower() == request.Country.ToLower() &&
                h.Accomodations.Any(a => a.Capacity == request.Guests && a.Occupancies.All(occ =>
                occ.AccomodationId == a.Id &&
                !(occ.StartDate >= request.StartDate && occ.StartDate < request.EndDate) &&
                !(occ.EndDate >= request.StartDate && occ.EndDate < request.EndDate))))
            .ToListAsync(cancellationToken);


        var result = new GetAccomodationsResponse();

        var days = (request.EndDate - request.StartDate).Days;

        foreach (var hotel in hotels)
        {
            result.Hotels.Add(new HotelDto()
            {
                Id = hotel.Id,
                CountOfStars = hotel.CountOfStars,
                LowestPrice = await _context.Accomodations.Where(a => a.HotelId == hotel.Id)
                                                          .Select(a => a.PricePerDay)
                                                          .MinAsync(cancellationToken) * days,
                Name = hotel.Name,
                PreviewImage = hotel.PreviewImageId
            });
        }
        return result;
    }
}