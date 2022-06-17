using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class UpdateAccomodationAttributeHandler: IRequestHandler<UpdateAccomodationAttributeRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public UpdateAccomodationAttributeHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateAccomodationAttributeRequest request, CancellationToken cancellationToken)
    {
        var attribute = await _context.AccomodationsAttributes
            .FirstOrDefaultAsync(att => att.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (attribute == null)
        {
            attribute = new AccomodationsAttribute()
            {
                Name = request.Name,
                MeasureUnit = request.MeasureOfUnit,
                ValuesAccomodationAttributes = new List<ValuesAccomodationAttribute>()
                {
                    new ValuesAccomodationAttribute()
                    {
                        AccomodationId = request.AccomodationId,
                        Value = request.Value
                    }
                }
            };
            _context.Add(attribute);
        }
        else
        {
            attribute.Name = request.Name;
            attribute.MeasureUnit = request.MeasureOfUnit;

            var value = await _context.ValuesAccomodationAttributes.FirstOrDefaultAsync(
                val => val.AccomodationId == request.AccomodationId && val.AccomodationAttributeId == attribute.Id, cancellationToken);

            if (value == null)
            {
                value = new ValuesAccomodationAttribute()
                {
                    AccomodationId = request.AccomodationId,
                    AccomodationAttributeId = attribute.Id,
                    Value = request.Value
                };

                _context.Add(value);
            }
            else
            {
                value.Value = request.Value;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}