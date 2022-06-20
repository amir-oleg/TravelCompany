using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class AddTourHandler:IRequestHandler<AddTourRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public AddTourHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddTourRequest request, CancellationToken cancellationToken)
    {
        var way = await _context.Ways.FirstOrDefaultAsync(
            way => way.StartCityId == request.StartCityId && way.EndCityId == request.EndCityId &&
                   way.TransportTypeCode == request.TransportType, cancellationToken) ?? new Way()
        {
            StartCityId = request.StartCityId,
            EndCityId = request.EndCityId,
            TransportTypeCode = request.TransportType,
        };

        var tourCategories = await _context.TourCategories.Where(tour => request.TourCategoryCodes.Contains(tour.Code))
            .ToListAsync(cancellationToken);

        var tour = new Tour()
        {
            AccomodationId = request.AccomodationId,
            ChildrenCount = request.ChildrenCount,
            Cost = request.Price,
            Days = request.Days,
            DietCode = request.DietTypeCode,
            GuestsCount = request.GuestsCount,
            Name = request.Name,
            Way = way,
            TourCategoryCodes = tourCategories,
            PreviewImage = new Image()
            {
                ImageBytes = Convert.FromBase64String(request.Image)
            }
        };

        var attNames = request.Attributtes.Select(att => att.Name.ToLower());

        var attributes = await _context.ToursAttributes
            .Where(att => attNames.Contains(att.Name.ToLower())).AsNoTracking().ToListAsync(cancellationToken);

        var tourAttributes = new List<ValuesToursAttribute>();
        var remainAtts = request.Attributtes.ToList();
        foreach (var attribute in attributes)
        {
            var reqAtt = request.Attributtes.Single(att => att.Name.ToLower() == attribute.Name.ToLower());
            remainAtts.Remove(remainAtts.Find(att => att.Name.ToLower() == reqAtt.Name.ToLower()));
            attribute.Name = reqAtt.Name;
            attribute.MeasureUnit = reqAtt.MeasureOfUnit;

            tourAttributes.Add( new ValuesToursAttribute()
            {
                TourAttributeId = attribute.Id,
                Value = reqAtt.Value
            });
        }

        foreach (var att in remainAtts)
        {
            tourAttributes.Add(new ValuesToursAttribute()
            {
                Value = att.Value,
                TourAttribute = new ToursAttribute()
                {
                    Name = att.Name,
                    MeasureUnit = att.Name
                }
            });
        }

        tour.ValuesToursAttributes = tourAttributes;

        _context.Tours.Add(tour);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}