using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetPersonalDataHandler: IRequestHandler<GetPersonalDataRequest, GetPersonalResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetPersonalDataHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetPersonalResponse> Handle(GetPersonalDataRequest request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.SingleAsync(cl => cl.Email == request.Email, cancellationToken);

        var orders = await _context.Orders
            .Include(ord => ord.Tour)
            .Include(ord => ord.Accomodation)
            .ThenInclude(acc => acc.Type)
            .ThenInclude(act => act.Hotel)
            .Where(ord => ord.ClientId == client.Id)
            .OrderByDescending(ord => ord.StartDate)
            .ToListAsync(cancellationToken);

        var response = new GetPersonalResponse()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Patronymic = client.Patronymic
        };

        foreach (var order in orders)
        {
            
            var res = new OrderResponse()
            {
                TourName = order.Tour?.Name,
                HotelName = order.Accomodation.Type.Hotel.Name,
                AccomodationName = order.Accomodation.Type.Name,
                StartDate = order.StartDate.ToString("dd-MM-yyyy"),
                EndDate = order.EndDate.ToString("dd-MM-yyyy"),
                Id = order.Id,
                IsPaid = order.IsPaid
            };
            if (order.Tour != null)
                res.Price = order.Tour.Cost;
            else
                res.Price = order.Accomodation.Type.PricePerDay * (order.EndDate - order.StartDate).Days;
            response.Orders.Add(res);
        }

        return response;
    }
}