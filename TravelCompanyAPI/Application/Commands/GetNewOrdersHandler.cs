using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetNewOrdersHandler: IRequestHandler<GetNewOrdersRequest, GetNewOrdersResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetNewOrdersHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetNewOrdersResponse> Handle(GetNewOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(ord => ord.Tour)
            .Include(ord => ord.Client)
            .Where(ord => ord.EmployeeId == null)
            .ToListAsync(cancellationToken);

        var response = new GetNewOrdersResponse();

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
                ClientPhoneNumber = order.Client.Phone
            });
        }

        return response;
    }
}