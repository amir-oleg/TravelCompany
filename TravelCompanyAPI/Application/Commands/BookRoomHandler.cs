using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.EntitiesEav;

namespace TravelCompanyAPI.Application.Commands;

public class BookRoomHandler: IRequestHandler<BookRoomRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public BookRoomHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(BookRoomRequest request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.SingleAsync(cl => cl.Email == request.Email, cancellationToken);
        var accomodation = await _context.Accomodations.SingleAsync(acc => acc.Id == request.AccomodationId, cancellationToken);

        _context.Orders.Add(new Order
        {
            ClientId = client.Id,
            AccomodationId = request.AccomodationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Cost = (request.EndDate - request.StartDate).Days * accomodation.PricePerDay,
            Date = DateTime.UtcNow
        });

        _context.Occupancies.Add(new Occupancy
        {
            AccomodationId = accomodation.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        });

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}