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

            var attributes = await _context.HotelsAttributes
                .Where(att => attNames.Contains(att.Name.ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            var hotelAttributes = new List<ValuesHotelsAttribute>();
            var remainAtts = request.HotelAttributes.ToList();
            foreach (var attribute in attributes)
            {
                var reqAtt = request.HotelAttributes.First(att => att.Name.ToLower() == attribute.Name.ToLower());
                remainAtts.Remove(remainAtts.Find(att => att.Name.ToLower() == reqAtt.Name.ToLower()));
                attribute.Name = reqAtt.Name;
                attribute.MeasureUnit = reqAtt.MeasureOfUnit;

                hotelAttributes.Add(new ValuesHotelsAttribute()
                {
                    HotelAttributeId = attribute.Id,
                    Value = reqAtt.Value
                });
            }

            foreach (var att in remainAtts)
            {
                hotelAttributes.Add(new ValuesHotelsAttribute()
                {
                    Value = att.Value,
                    HotelAttribute = new HotelsAttribute()
                    {
                        Name = att.Name,
                        MeasureUnit = att.MeasureOfUnit
                    }
                });
            }

            hotel.ValuesHotelsAttributes = hotelAttributes;
        }

        int accAttsId = 0;

        foreach (var accomodation in hotel.AccomodationTypes)
        {
            
            var attNames = request.Accomodations[accAttsId].Attributes.Select(att => att.Name.ToLower());

            var attributes = await _context.AccomodationsAttributes
                .Where(att => attNames.Contains(att.Name.ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            var accAttributes = new List<ValuesAccomodationAttribute>();
            var remainAtts = request.Accomodations[accAttsId].Attributes.ToList();
            foreach (var attribute in attributes)
            {
                var reqAtt = request.Accomodations[accAttsId].Attributes.First(att => att.Name.ToLower() == attribute.Name.ToLower());
                remainAtts.Remove(remainAtts.Find(att => att.Name.ToLower() == reqAtt.Name.ToLower()));
                attribute.Name = reqAtt.Name;
                attribute.MeasureUnit = reqAtt.MeasureOfUnit;

                accAttributes.Add(new ValuesAccomodationAttribute()
                {
                    AccomodationAttributeId = attribute.Id,
                    Value = reqAtt.Value
                });
            }

            foreach (var att in remainAtts)
            {
                accAttributes.Add(new ValuesAccomodationAttribute()
                {
                    Value = att.Value,
                    AccomodationAttribute = new AccomodationsAttribute()
                    {
                        Name = att.Name,
                        MeasureUnit = att.MeasureOfUnit
                    }
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