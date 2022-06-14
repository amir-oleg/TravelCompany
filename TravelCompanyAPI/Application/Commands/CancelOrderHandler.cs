using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class CancelOrderHandler: IRequestHandler<CancelOrderRequest>
{
    private readonly TravelCompanyClassicContext _context;

    public CancelOrderHandler(TravelCompanyClassicContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.SingleAsync(ord => ord.Id == request.OrderId, cancellationToken);
        var occupancy = await _context.Occupancies.SingleAsync(
            occ => occ.AccomodationId == order.AccomodationId && occ.StartDate == order.StartDate &&
                   occ.EndDate == order.EndDate, cancellationToken);

        _context.Orders.Remove(order);
        _context.Occupancies.Remove(occupancy);

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}