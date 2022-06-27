using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Application.Commands;

public class CreateOrderHandler:IRequestHandler<CreateOrderRequest>
{
    private readonly TravelCompanyEAVContext _context;

    public CreateOrderHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var userId = await _context.Clients
            .Select(cl => new {cl.Email, cl.Id})
            .SingleAsync(cl => cl.Email.ToLower() == request.UserEmail.ToLower(), cancellationToken);

        var order = new Order()
        {
            AccomodationId = request.AccomodationId,
            ClientId = userId.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreationDate = DateTime.Now
        };

        var occupancy = new Occupancy
        {
            AccomodationId = request.AccomodationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        if (request.TourId != default)
        {
            order.TourId = request.TourId;

            var ids = await _context.Tours
                .Include(tour => tour.AccomodationType)
                .ThenInclude(act => act.Accomodations)
                .ThenInclude(acc => acc.Occupancies)
                .Where(tour => tour.Id == request.TourId && tour.AccomodationType.Accomodations.Any(acc =>
                    acc.Occupancies.All(occ =>
                        !(occ.StartDate >= request.StartDate && occ.StartDate < request.EndDate) &&
                        !(occ.EndDate >= request.StartDate && occ.EndDate < request.EndDate))))
                .Select(tour => new
                    { TypeId = tour.AccomodationTypeId, AccId = tour.AccomodationType.Accomodations.First().Id })
                .FirstOrDefaultAsync(cancellationToken);
            occupancy.AccomodationId = ids.AccId;
            order.AccomodationId = ids.TypeId;
        }

        

        _context.Add(order);
        _context.Add(occupancy);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}