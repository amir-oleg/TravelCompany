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
        var managerStats = await _context.Orders
            .Include(order => order.Tour)
            .Where(ord => ord.IsPaid && ord.EmployeeId != null)
            .GroupBy(ord => ord.EmployeeId)
            .Select(ord => new { ord.Key, Income = ord.Sum(ord => ord.Tour.Cost) })
            .ToListAsync(cancellationToken);

        var managers = await _context.Employees
            .Where(emp => managerStats.Select(ms => ms.Key).Contains(emp.Id))
            .ToListAsync(cancellationToken);

        return managerStats.Select(managerStat => new GetManagersStatisticsResponse
        {
            ManagerName = $"{managers.Single(mng => mng.Id == managerStat.Key).LastName} {managers.Single(mng => mng.Id == managerStat.Key).FirstName} {managers.Single(mng => mng.Id == managerStat.Key).Patronymic}",
            Income = managerStat.Income
        }).ToList();
    }
}