using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

internal class GetAccomodationsInHotelHandler: IRequestHandler<GetAccomodationsInHotelRequest, List<GetAccomodationInHotelResponse>>
{
    private readonly TravelCompanyClassicContext _context;

    public GetAccomodationsInHotelHandler(TravelCompanyClassicContext context)
    {
        _context = context;
    }

    public async Task<List<GetAccomodationInHotelResponse>> Handle(GetAccomodationsInHotelRequest request, CancellationToken cancellationToken)
    {
        var accomodations = await _context.Accomodations
            .Include(acc => acc.Occupancies)
            .Include(acc => acc.Images)
            .Where(acc => acc.HotelId == request.HotelId &&
                          acc.Capacity == request.Guests && 
                          acc.Occupancies.All(occ =>
                              occ.AccomodationId == acc.Id &&
                              !(occ.StartDate >= request.StartDate && occ.StartDate < request.EndDate) &&
                              !(occ.EndDate >= request.StartDate && occ.EndDate < request.EndDate)))
            .Select(acc => new 
            {
                acc.Id,
                acc.Capacity,
                acc.Name,
                acc.PricePerDay,
                acc.IsAcExists,
                acc.IsWcExixts,
                acc.IsWifiExists,
                Images = acc.Images.Select(i => i.Id)
            })
            .ToListAsync(cancellationToken);

        var days = (request.EndDate - request.StartDate).Days;

        return accomodations.Select(accomodation => new GetAccomodationInHotelResponse
        {
            Id = accomodation.Id,
            Capacity = accomodation.Capacity, 
            Name = accomodation.Name,
            Price = accomodation.PricePerDay * days,
            IsAcExists = accomodation.IsAcExists,
            IsWcExixts = accomodation.IsWcExixts,
            IsWifiExists = accomodation.IsWifiExists,
            Images = accomodation.Images
        }).ToList();
    }
}