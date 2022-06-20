using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetAccomodationsInHotelHandler:IRequestHandler<GetAccomodationsInHotelRequest, List<GetAccomodationsInHotelResponse>>
{
    private readonly TravelCompanyEAVContext _context;
    public GetAccomodationsInHotelHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }


    public async Task<List<GetAccomodationsInHotelResponse>> Handle(GetAccomodationsInHotelRequest request, CancellationToken cancellationToken)
    {
        var accomodations = await _context.AccomodationTypes
            .Include(act => act.Accomodations)
            .ThenInclude(acc => acc.Occupancies)
            .Include(acc => acc.Images)
            .Include(acc => acc.ValuesAccomodationAttributes)
            .ThenInclude(vaa => vaa.AccomodationAttribute)
            .Where(acc => acc.HotelId == request.HotelId)
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


        var response = new List<GetAccomodationsInHotelResponse>();

        foreach (var accomodation in accomodations)
        {
            response.Add(new GetAccomodationsInHotelResponse()
            {
                Id = accomodation.Id,
                Capacity = accomodation.Capacity,
                Name = accomodation.Name,
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