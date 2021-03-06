using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetManagerDataHandler: IRequestHandler<GetManagerDataRequest, GetManagerDataResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetManagerDataHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetManagerDataResponse> Handle(GetManagerDataRequest request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.SingleAsync(cl => cl.Email == request.Email, cancellationToken);

        var orders = await _context.Orders
            .Include(ord => ord.Tour)
            .Include(ord => ord.Client)
            .Where(ord => ord.EmployeeId == employee.Id)
            .ToListAsync(cancellationToken);

        var response = new GetManagerDataResponse()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Patronymic = employee.Patronymic
        };

        foreach (var order in orders)
        {
            response.Orders.Add(new ManagerOrderResponse()
            {
                TourName = order.Tour.Name,
                StartDate = order.StartDate.ToString("dd-MM-yyyy"),
                EndDate = order.EndDate.ToString("dd-MM-yyyy"),
                Id = order.Id,
                Price = order.Tour.Cost,
                ClientName = order.Client.FirstName,
                ClientPhoneNumber = order.Client.Phone,
                IsPaid = order.IsPaid
            });
        }

        return response;
    }
}