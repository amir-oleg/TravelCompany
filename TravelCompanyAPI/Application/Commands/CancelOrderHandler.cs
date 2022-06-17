using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class CancelOrderHandler: IRequestHandler<CancelOrderRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public CancelOrderHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(ord => ord.Tour)
            .ThenInclude(tour => tour.Accomodations)
            .SingleAsync(ord => ord.Id == request.OrderId, cancellationToken);
        var ids = order.Tour.Accomodations.Select(acc => acc.Id).ToList();
        var occupancies = await _context.Occupancies
            .Where(
                occ => ids.Contains(occ.AccomodationId) && occ.StartDate == order.StartDate &&
                       occ.EndDate == order.EndDate)
            .ToListAsync(cancellationToken);

        _context.Orders.Remove(order);
        _context.Occupancies.RemoveRange(occupancies);

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}