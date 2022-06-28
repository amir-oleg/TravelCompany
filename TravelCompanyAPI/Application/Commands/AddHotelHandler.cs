using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class AddHotelHandler:IRequestHandler<AddHotelRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public AddHotelHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddHotelRequest request, CancellationToken cancellationToken)
    {
        var hotel = new Hotel
        {
            Name = request.HotelName,
            CategoryCode = request.CategoryCode,
            CityId = request.City,
            PreviewImage = new Image
            {
                ImageBytes = request.PreviewImageBytes
            },
            AccomodationTypes = request.Accomodations.Select(acc => new AccomodationType()
            {
                Name = acc.Name,
                Capacity = acc.Capacity,
                PricePerDay = acc.PricePerDay,
                Images = acc.Images.Select(img => new Image()
                {
                    ImageBytes = img
                }).ToList(),
                Accomodations = GetEmptyAccomodations(acc.Count)
            }).ToList()
        };

        {
            var attNames = request.HotelAttributes.Select(att => att.Name.ToLower());

            var existingAttributes = await _context.HotelsAttributes
                .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var attributte in request.HotelAttributes)
            {
                if (existingAttributes.All(ea => ea.Name.ToLower() != attributte.Name.Trim().ToLower()))
                {
                    _context.Add(new HotelsAttribute
                    {
                        Name = attributte.Name.Trim(),
                        MeasureUnit = attributte.MeasureOfUnit.Trim()
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            existingAttributes = await _context.HotelsAttributes
                .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            var hotelsAttributes = new List<ValuesHotelsAttribute>();

            foreach (var attribute in existingAttributes)
            {
                var reqAtt = request.HotelAttributes.First(att => att.Name.Trim().ToLower() == attribute.Name.ToLower());

                hotelsAttributes.Add(new ValuesHotelsAttribute()
                {
                    HotelAttributeId = attribute.Id,
                    Value = reqAtt.Value.Trim()
                });
            }
            hotel.ValuesHotelsAttributes = hotelsAttributes;
        }

        foreach (var accomodation in request.Accomodations)
        {
            
            var attNames = accomodation.Attributes.Select(att => att.Name.ToLower());

            var existingAttributes = await _context.AccomodationsAttributes
                .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var attributte in accomodation.Attributes)
            {
                if (existingAttributes.All(ea => ea.Name.ToLower() != attributte.Name.Trim().ToLower()))
                {
                    _context.Add(new HotelsAttribute
                    {
                        Name = attributte.Name.Trim(),
                        MeasureUnit = attributte.MeasureOfUnit.Trim()
                    });                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        var accAttsId = 0;

        foreach (var accomodation in hotel.AccomodationTypes)
        {

            var attNames = request.Accomodations[accAttsId].Attributes.Select(att => att.Name.ToLower());

            var existingAttributes = await _context.AccomodationsAttributes
                .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            var accAttributes = new List<ValuesAccomodationAttribute>();

            foreach (var attribute in existingAttributes)
            {
                var reqAtt = request.Accomodations[accAttsId].Attributes.First(att => att.Name.Trim().ToLower() == attribute.Name.ToLower());

                accAttributes.Add(new ValuesAccomodationAttribute
                {
                    AccomodationAttributeId = attribute.Id,
                    Value = reqAtt.Value.Trim()
                });
            }

            accomodation.ValuesAccomodationAttributes = accAttributes;

            accAttsId++;
        }
        _context.Add(hotel);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private List<Accomodation> GetEmptyAccomodations(int count)
    {
        var list = new List<Accomodation>();
        for (var i = 0; i < count; i++)
        {
            list.Add(new Accomodation()
            {
                Number = i + 1
            });
        }
        return list;
    }
}