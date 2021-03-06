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
            AccomodationTypeId = request.AccomodationId,
            ChildrenCount = request.ChildrenCount,
            Cost = request.Price,
            Days = request.Days,
            DietCode = request.DietTypeCode,
            GuestsCount = request.GuestsCount,
            Name = request.Name,
            Way = way,
            TourCategoryCodes = tourCategories,
            PreviewImage = new Image
            {
                ImageBytes = Convert.FromBase64String(request.Image)
            }
        };

        var attNames = request.Attributtes.Select(att => att.Name.ToLower());

        var existingAttributes = await _context.ToursAttributes
            .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

        foreach (var attributte in request.Attributtes)
        {
            if (existingAttributes.All(ea => ea.Name.ToLower() != attributte.Name.Trim().ToLower()))
            {
                _context.Add(new ToursAttribute
                {
                    Name = attributte.Name.Trim(),
                    MeasureUnit = attributte.MeasureOfUnit.Trim()
                });
            }
        }
        await _context.SaveChangesAsync(cancellationToken);


        existingAttributes = await _context.ToursAttributes
            .Where(att => attNames.Contains(att.Name.Trim().ToLower())).AsNoTracking().ToListAsync(cancellationToken);

        var tourAttributes = new List<ValuesTourAttribute>();
        
        foreach (var attribute in existingAttributes)
        {
            var reqAtt = request.Attributtes.First(att => att.Name.Trim().ToLower() == attribute.Name.ToLower());

            tourAttributes.Add( new ValuesTourAttribute
            {
                TourAttributeId = attribute.Id,
                Value = reqAtt.Value.Trim()
            });
        }

        tour.ValuesTourAttributes = tourAttributes;

        _context.Tours.Add(tour);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}