using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class MarkOrderAsPaidHandler: IRequestHandler<MarkOrderAsPaidRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public MarkOrderAsPaidHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(MarkOrderAsPaidRequest request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.SingleAsync(ord => ord.Id == request.OrderId, cancellationToken);

        order.IsPaid = true;

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}