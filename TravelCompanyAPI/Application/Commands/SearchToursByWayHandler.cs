using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyAPI.Helpers;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class SearchToursByWayHandler: IRequestHandler<SearchToursByWayRequest, SearchToursByWayResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public SearchToursByWayHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<SearchToursByWayResponse> Handle(SearchToursByWayRequest request, CancellationToken cancellationToken)
    {
        var thisMany = (request.Page - 1) * GeneralConsts.PageSize;

        var endDate = request.StartDate.AddDays(request.Days);

        var tours = _context.Tours.Include(tour => tour.Way)
            .ThenInclude(way => way.StartCity)
            .ThenInclude(startCity => startCity.Country)
            .Include(tour => tour.Way)
            .ThenInclude(way => way.EndCity)
            .ThenInclude(endCity => endCity.Country)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Occupancies)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Hotel)
            .Include(tour => tour.TourCategoryCodes)
            .Include(tour => tour.DietCodeNavigation)
            .Where(tour => Math.Abs(tour.Days - request.Days) <= 3 && tour.Cost >= request.PriceFrom &&
                           tour.Cost <= request.PriceTo && tour.GuestsCount == request.GuestsCount &&
                           tour.ChildrenCount == request.ChildrenCount && request.DietTypes.Contains(tour.DietCode) &&
                           tour.TourCategoryCodes.Any(tc => request.TourCategories.Contains(tc.Code)) &&
                           request.HotelCategories.Contains(tour.Accomodation.Hotel.CategoryCode) &&
                           (tour.Way.StartCity.Name.ToLower() == request.StartPlace &&
                            tour.Way.EndCity.Name.ToLower() == request.EndPlace ||
                            tour.Way.StartCity.Name.ToLower() == request.StartPlace &&
                            tour.Way.EndCity.Country.Name.ToLower() == request.EndPlace ||
                            tour.Way.StartCity.Country.Name.ToLower() == request.StartPlace &&
                            tour.Way.EndCity.Name.ToLower() == request.EndPlace ||
                            tour.Way.StartCity.Country.Name.ToLower() == request.StartPlace &&
                            tour.Way.EndCity.Country.Name.ToLower() == request.EndPlace) &&
                           request.TransportType.Contains(tour.Way.TransportTypeCode) && tour.Accomodation.Occupancies.All(
                               occ => occ.AccomodationId == tour.Accomodation.Id &&
                                      !(occ.StartDate >= request.StartDate && occ.StartDate < endDate) &&
                                      !(occ.EndDate >= request.StartDate && occ.EndDate < endDate)));

        double count = await tours.CountAsync(cancellationToken);

        var results = await tours.Skip(thisMany)
            .Take(GeneralConsts.PageSize)
            .AsNoTracking()
            .Select(tour => new
            {
                tour.Id,
                tour.Name,
                tour.Cost,
                DietType = tour.DietCodeNavigation.Value,
                tour.PreviewImageId
            })
            .ToListAsync(cancellationToken);

        return new SearchToursByWayResponse
        {
            PageCount = (int)Math.Ceiling(count / GeneralConsts.PageSize),
            Tours = results.Select(res => new TourResponse()
            {
                Id = res.Id,
                Name = res.Name,
                Price = res.Cost,
                DietType = res.DietType,
                PreviewImageId = res.PreviewImageId
            }).ToList()
        };
    }
}