using MediatR;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyAPI.Helpers;

namespace TravelCompanyAPI.Application.Commands;

public class SearchToursByWayRequest: IRequest<SearchToursByWayResponse>
{
    public SearchToursByWayRequest(string startPlace, string endPlace, string[] transportType, int guestsCount, int childrenCount, DateTime startDate, int days, List<string> dietTypes, List<string> hotelCategories, List<string> tourCategories, decimal? priceFrom, decimal? priceTo, int page)
    {
        StartPlace = startPlace;
        EndPlace = endPlace;
        TransportType = transportType == null || transportType.Length == 0 ? TrasnportTypesConsts.GetAll().ToArray() : transportType;
        GuestsCount = guestsCount;
        ChildrenCount = childrenCount;
        StartDate = startDate;
        Days = days;
        DietTypes = dietTypes == null || dietTypes.Count == 0 ? DietTypesConsts.GetAllDiets().ToList() : dietTypes;
        HotelCategories = hotelCategories == null || hotelCategories.Count == 0 ? HotelCategoriesConsts.GetAllCategories().ToList() : hotelCategories; ;
        TourCategories = tourCategories == null || tourCategories.Count == 0 ? TourCategoriesConsts.GetAllCategories().ToList() : tourCategories; ;
        PriceFrom = priceFrom ?? 0;
        PriceTo = priceTo ?? decimal.MaxValue;
        Page = page;
    }

    public string StartPlace { get; set; }
    public string EndPlace { get; set; }
    public string[] TransportType { get; set; }
    public int GuestsCount { get; set; }
    public int ChildrenCount { get; set; }
    public DateTime StartDate { get; set; }
    public int Days { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string> HotelCategories { get; set; }
    public List<string> TourCategories { get; set; }
    public decimal PriceFrom { get; set; }
    public decimal PriceTo { get; set; }
    public int Page { get; set; }
}