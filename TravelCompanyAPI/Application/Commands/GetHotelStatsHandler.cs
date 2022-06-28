using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetHotelStatsHandler: IRequestHandler<GetHotelStatsRequest, GetHotelStatsResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetHotelStatsHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetHotelStatsResponse> Handle(GetHotelStatsRequest request, CancellationToken cancellationToken)
    {
        var accomodationTypes = await _context.AccomodationTypes
            .Include(act => act.Accomodations)
            .ThenInclude(acc => acc.Occupancies)
            .Where(act => act.HotelId == request.HotelId)
            .ToListAsync(cancellationToken);

        var free = 0;
        var busy = 0;
        foreach (var accomodationType in accomodationTypes)
        {
            free += accomodationType.Accomodations.Count(acc => acc.Occupancies.All(occ =>
                occ.AccomodationId == acc.Id &&
                !(occ.StartDate >= DateTime.Now && occ.StartDate < DateTime.Now) &&
                !(occ.EndDate >= DateTime.Now && occ.EndDate < DateTime.Now)));
            busy += accomodationType.Accomodations.Count;
        }

        busy -= free;

        return new GetHotelStatsResponse() { Busy = busy, Free = free };

    }
}