using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetPersonalDataHandler: IRequestHandler<GetPersonalDataRequest, GetPersonalResponse>
{
    private readonly TravelCompanyClassicContext _context;

    public GetPersonalDataHandler(TravelCompanyClassicContext context)
    {
        _context = context;
    }

    public async Task<GetPersonalResponse> Handle(GetPersonalDataRequest request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.SingleAsync(cl => cl.Email == request.Email, cancellationToken);

        var orders = await _context.Orders
            .Include(ord => ord.Accomodation)
            .ThenInclude(acc => acc.Hotel)
            .Where(ord => ord.ClientId == client.Id)
            .ToListAsync(cancellationToken);

        var response = new GetPersonalResponse()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Patronymic = client.Patronymic
        };

        foreach (var order in orders)
        {
            response.Orders.Add(new OrderResponse()
            {
                AccomodationName = order.Accomodation.Name,
                HotelName = order.Accomodation.Hotel.Name,
                StartDate = order.StartDate.ToString("dd-MM-yyyy"),
                EndDate = order.EndDate.ToString("dd-MM-yyyy"),
                Id = order.Id,
                Price = order.Cost
            });
        }

        return response;
    }
}