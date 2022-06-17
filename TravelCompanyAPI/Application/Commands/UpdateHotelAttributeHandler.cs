using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class UpdateHotelAttributeHandler: IRequestHandler<UpdateHotelAttributeRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public UpdateHotelAttributeHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHotelAttributeRequest request, CancellationToken cancellationToken)
    {
        var attribute = await _context.HotelsAttributes           
            .FirstOrDefaultAsync(att => att.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (attribute == null)
        {
            attribute = new HotelsAttribute()
            {
                Name = request.Name,
                MeasureUnit = request.MeasureOfUnit,
                ValuesHotelsAttributes = new List<ValuesHotelsAttribute>()
                {
                    new ValuesHotelsAttribute()
                    {
                        HotelId = request.HotelId,
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

            var value = await _context.ValuesHotelsAttributes.FirstOrDefaultAsync(
                val => val.HotelId == request.HotelId && val.HotelAttributeId == attribute.Id, cancellationToken);

            if (value == null)
            {
                value = new ValuesHotelsAttribute()
                {
                    HotelId = request.HotelId,
                    HotelAttributeId = attribute.Id,
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