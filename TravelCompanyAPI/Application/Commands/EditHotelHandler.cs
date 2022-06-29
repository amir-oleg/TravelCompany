using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class EditHotelHandler : IRequestHandler<EditHotelRequest>
{

    private readonly TravelCompanyEAVContext _context;

    public EditHotelHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }


    public async Task<Unit> Handle(EditHotelRequest request, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels
            .Include(hotel=> hotel.AccomodationTypes)
            .ThenInclude(act=> act.ValuesAccomodationAttributes)
            .Include(hotel => hotel.ValuesHotelsAttributes)
            .SingleAsync(hotel => hotel.Id==request.Id,cancellationToken);


        hotel.Name = request.HotelName;
        if (request.CategoryCode?.Length == 2)
        { hotel.CategoryCode = request.CategoryCode; }
        if (int.TryParse(request.City, out var id))
        { hotel.CityId = id; }
        if (request.PreviewImageBytes != null)
        hotel.PreviewImage = new Image
        {
            ImageBytes = request.PreviewImageBytes
        };
        foreach(var act in hotel.AccomodationTypes)
        {
            var actEdit = request.Accomodations.First(acc => acc.Id == act.Id);
            act.Name = actEdit.Name;
            act.Capacity = actEdit.Capacity;
            act.PricePerDay = actEdit.PricePerDay;
        }
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

            foreach (var attribute in existingAttributes)
            {
                var reqAtt = request.HotelAttributes.First(att => att.Name.Trim().ToLower() == attribute.Name.ToLower());

                var vha = hotel.ValuesHotelsAttributes.FirstOrDefault(vah => vah.HotelAttributeId == attribute.Id);
                if (vha != null)
                {
                    vha.Value = reqAtt.Value.Trim();
                }
                else
                {
                    hotel.ValuesHotelsAttributes.Add(new ValuesHotelsAttribute()
                    {
                        HotelAttributeId = attribute.Id,
                        Value = reqAtt.Value.Trim()
                    });
                }
            }
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
                    });
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        var accAttsId = 0;

        foreach (var accomodation in hotel.AccomodationTypes)
        {

            var attNames = request.Accomodations[accAttsId].Attributes.Select(att => att.Name.ToLower());

            var existingAttributes = await _context.AccomodationsAttributes
                .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var attribute in existingAttributes)
            {
                var reqAtt = request.Accomodations[accAttsId].Attributes.First(att => att.Name.Trim().ToLower() == attribute.Name.ToLower());

                var vaa = accomodation.ValuesAccomodationAttributes.FirstOrDefault(vah => vah.AccomodationAttributeId == attribute.Id);
                if (vaa != null)
                {
                    vaa.Value = reqAtt.Value.Trim();
                }
                else
                {
                    hotel.AccomodationTypes.ElementAt(accAttsId).ValuesAccomodationAttributes.Add(new ValuesAccomodationAttribute()
                    {
                        AccomodationAttributeId = attribute.Id,
                        Value = reqAtt.Value.Trim()
                    });
                }

            }

            accAttsId++;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
