using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetFreeOrdersHandler: IRequestHandler<GetFreeOrdersRequest, GetFreeOrdersResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetFreeOrdersHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetFreeOrdersResponse> Handle(GetFreeOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(ord => ord.Tour)
            .Include(ord => ord.Client)
            .Where(ord => ord.EmployeeId == null)
            .ToListAsync(cancellationToken);

        var response = new GetFreeOrdersResponse();

        foreach (var order in orders)
        {
            response.Orders.Add(new ManagerOrderResponse()
            {
                TourName = order.Tour.Name,
                ClientName = order.Client.FirstName,
                ClientPhoneNumber = order.Client.Phone,
                StartDate = order.StartDate.ToString("dd-MM-yyyy"),
                EndDate = order.EndDate.ToString("dd-MM-yyyy"),
                Id = order.Id,
                Price = order.Tour.Cost
            });
        }

        return response;
    }
}