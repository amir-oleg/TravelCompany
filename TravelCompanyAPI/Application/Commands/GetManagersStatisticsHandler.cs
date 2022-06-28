using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetManagersStatisticsHandler: IRequestHandler<GetManagersStatisticsRequest, IEnumerable<GetManagersStatisticsResponse>>
{
    private readonly TravelCompanyEAVContext _context;

    public GetManagersStatisticsHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetManagersStatisticsResponse>> Handle(GetManagersStatisticsRequest request, CancellationToken cancellationToken)
    {
        var date = request.Month.Split('-');
        var year = int.Parse(date[0]);
        var month = int.Parse(date[1]);
        var orders = await _context.Orders
            .Include(order => order.Tour)
            .Include(ord => ord.Accomodation)
            .ThenInclude(acc => acc.Type)
            .Where(ord => ord.IsPaid && ord.EmployeeId != null && ord.CreationDate.Year == year &&
                          ord.CreationDate.Month == month)
            .ToListAsync(cancellationToken);

        var managerStats = new Dictionary<int?, decimal?>();

        foreach (var order in orders)
        {
            if (managerStats.TryGetValue(order.EmployeeId, out _))
            {
                if (order.Tour == null)
                {
                    managerStats[order.EmployeeId] += order.Accomodation.Type.PricePerDay * (order.EndDate - order.StartDate).Days;
                }
                else
                {
                    managerStats[order.EmployeeId] += order.Tour.Cost;
                }
            }
            else
            {
                if (order.Tour == null)
                {
                    managerStats.Add(order.EmployeeId,order.Accomodation.Type.PricePerDay * (order.EndDate - order.StartDate).Days);
                }
                else
                {
                    managerStats.Add(order.EmployeeId,managerStats[order.EmployeeId] += order.Tour.Cost);
                }
            }
        }

        var managers = await _context.Employees
            .Where(emp => managerStats.Select(ms => ms.Key).Contains(emp.Id))
            .ToListAsync(cancellationToken);

        return managerStats.Select(managerStat => new GetManagersStatisticsResponse
        {
            ManagerName = $"{managers.Single(mng => mng.Id == managerStat.Key).LastName} {managers.Single(mng => mng.Id == managerStat.Key).FirstName} {managers.Single(mng => mng.Id == managerStat.Key).Patronymic}",
            Income = managerStat.Value.Value
        }).ToList();
    }

    private class Stat
    {
        public int key { get; set; }
        public decimal? Income { get; set; }
    }
}