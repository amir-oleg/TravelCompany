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
        var country = await _context.Countries.FirstOrDefaultAsync(country => country.Name == request.Country, cancellationToken) ??
                      new Country { Name = request.Country };

        var city = await _context.Cities.FirstOrDefaultAsync(city => city.Name == request.City, cancellationToken) ??
                   new City { Name = request.City, Country = country };

        _context.Add(new Hotel
        {
            Name = request.HotelName,
            CategoryCode = request.CategoryCode,
            City = city,
            PreviewImage = new Image
            {
                ImageBytes = request.PreviewImageBytes
            }
        });

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}