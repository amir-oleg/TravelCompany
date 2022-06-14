using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetNewOrdersHandler: IRequestHandler<GetNewOrdersRequest, GetNewOrdersResponse>
{
    private readonly TravelCompanyClassicContext _context;

    public async Task<GetNewOrdersResponse> Handle(GetNewOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(ord => ord.Accomodation)
            .ThenInclude(acc => acc.Hotel)
            .Include(ord => ord.Client)
            .Where(ord => ord.EmployeeId == null)
            .ToListAsync(cancellationToken);

        var response = new GetNewOrdersResponse();

        foreach (var order in orders)
        {
            response.Orders.Add(new ManagerOrderResponse()
            {
                AccomodationName = order.Accomodation.Name,
                HotelName = order.Accomodation.Hotel.Name,
                StartDate = order.StartDate.ToString("dd-MM-yyyy"),
                EndDate = order.EndDate.ToString("dd-MM-yyyy"),
                Id = order.Id,
                Price = order.Cost,
                ClientName = order.Client.FirstName,
                ClientPhoneNumber = order.Client.Phone
            });
        }

        return response;
    }
}