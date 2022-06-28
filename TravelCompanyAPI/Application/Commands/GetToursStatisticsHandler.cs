using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetToursStatisticsHandler: IRequestHandler<GetToursStatisticsRequest, IEnumerable<GetToursStatisticsResponse>>
{
    private readonly TravelCompanyEAVContext _context;

    public GetToursStatisticsHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetToursStatisticsResponse>> Handle(GetToursStatisticsRequest request, CancellationToken cancellationToken)
    {
        var date = request.Month.Split('-');
        var year = int.Parse(date[0]);
        var month = int.Parse(date[1]);

        var stat = await _context.Orders
            .Include(ord => ord.Tour)
            .Where(ord => ord.IsPaid && ord.TourId != null && ord.CreationDate.Year == year && ord.CreationDate.Month == month)
            .Select(ord => new
            {
                ord.Tour.Name,
                ord.Tour.Cost
            })
            .GroupBy(ord => ord.Name)
            .Select(ord => new
            {
                TourName = ord.Key,
                Total = ord.Sum(o => o.Cost),
                CountOfOrders = ord.Count()
            })
            .ToListAsync(cancellationToken);

        return stat.Select(st => new GetToursStatisticsResponse()
        {
            TourName = st.TourName,
            CountOfOrders = st.CountOfOrders,
            Total = st.Total
        });
    }
}