using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class TakeOrderInJobHandler: IRequestHandler<TakeOrderInJobRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public TakeOrderInJobHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(TakeOrderInJobRequest request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.SingleAsync(emp => emp.Email == request.Email, cancellationToken);
        var order = await _context.Orders.SingleAsync(ord => ord.Id == request.OrderId, cancellationToken);

        order.EmployeeId = employee.Id;

        await _context.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}