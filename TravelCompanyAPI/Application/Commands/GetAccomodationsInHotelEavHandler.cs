using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsInHotelEavHandler: IRequestHandler<GetAccomodationsInHotelEavRequest, List<GetAccomodationsInHotelEavResponse>>
{
    private readonly TravelCompanyEAVContext _context;

    public GetAccomodationsInHotelEavHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<List<GetAccomodationsInHotelEavResponse>> Handle(GetAccomodationsInHotelEavRequest request, CancellationToken cancellationToken)
    {
        var accomodations = await _context.AccomodationTypes
            .Include(act => act.Accomodations)
            .ThenInclude(acc => acc.Occupancies)
            .Include(acc => acc.Images)
            .Include(acc => acc.ValuesAccomodationAttributes)
            .ThenInclude(vaa => vaa.AccomodationAttribute)
            .Where(act => act.HotelId == request.HotelId &&
                          act.Capacity == request.Guests &&
                          act.Accomodations.Any(acc => acc.Occupancies.All(occ =>
                              occ.AccomodationId == acc.Id &&
                              !(occ.StartDate >= request.StartDate && occ.StartDate < request.EndDate) &&
                              !(occ.EndDate >= request.StartDate && occ.EndDate < request.EndDate))))
            .Select(acc => new
            {
                acc.Id,
                acc.Capacity,
                acc.Name,
                acc.PricePerDay,
                acc.ValuesAccomodationAttributes,
                Images = acc.Images.Select(i => i.Id)
            })
            .ToListAsync(cancellationToken);

        var days = (request.EndDate - request.StartDate).Days;

        var response = new List<GetAccomodationsInHotelEavResponse>();

        foreach (var accomodation in accomodations)
        {
            response.Add(new GetAccomodationsInHotelEavResponse()
            {
                Id = accomodation.Id,
                Capacity = accomodation.Capacity,
                Name = accomodation.Name,
                Price = accomodation.PricePerDay * days,
                Images = accomodation.Images
            });

            foreach (var valuesAccomodationAttribute in accomodation.ValuesAccomodationAttributes)
            {
                response.Last().Services.Add(new ServiceResponse()
                {
                    Name = valuesAccomodationAttribute.AccomodationAttribute.Name,
                    MeasureOfUnit = valuesAccomodationAttribute.AccomodationAttribute.MeasureUnit,
                    Value = valuesAccomodationAttribute.Value
                });
            }
        }

        return response;
    }
}