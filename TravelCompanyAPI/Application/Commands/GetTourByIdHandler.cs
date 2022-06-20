using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands;

public class GetTourByIdHandler: IRequestHandler<GetTourByIdRequest, GetTourByIdResponse>
{
    private readonly TravelCompanyEAVContext _context;

    public GetTourByIdHandler(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    public async Task<GetTourByIdResponse> Handle(GetTourByIdRequest request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(tour => tour.Way)
                .ThenInclude(way => way.StartCity)
                    .ThenInclude(startCity => startCity.Country)
            .Include(tour => tour.Way)
                .ThenInclude(way => way.EndCity)
                    .ThenInclude(endCity => endCity.Country)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Type)
                .ThenInclude(acc => acc.Hotel)
                    .ThenInclude(hotel => hotel.CategoryCodeNavigation)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Type)
                .ThenInclude(acc => acc.Hotel)
                    .ThenInclude(hotel => hotel.City)
                        .ThenInclude(city => city.Country)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Type)
                .ThenInclude(acc => acc.Hotel)
                    .ThenInclude(hotel => hotel.ValuesHotelsAttributes)
                        .ThenInclude(val =>val.HotelAttribute)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Type)
                .ThenInclude(acc => acc.ValuesAccomodationAttributes)
                    .ThenInclude(val => val.AccomodationAttribute)
            .Include(tour => tour.Accomodation)
            .ThenInclude(acc => acc.Type)
                .ThenInclude(acc => acc.Images)
            .Include(tour => tour.TourCategoryCodes)
            .Include(tour => tour.DietCodeNavigation)
            .Include(tour => tour.ValuesTourAttributes)
                .ThenInclude(vaa => vaa.TourAttribute)
            .SingleAsync(tour => tour.Id == request.TourId, cancellationToken);

        return new GetTourByIdResponse()
        {
            TourName = tour.Name,
            StartCountry = tour.Way.StartCity.Country.Name,
            EndCountry = tour.Way.EndCity.Country.Name,
            StartCity = tour.Way.StartCity.Name,
            EndCity = tour.Way.EndCity.Name,
            Price = tour.Cost,
            DietType = tour.DietCodeNavigation.Value,
            TourCategories = tour.TourCategoryCodes.Select(cat => cat.Value).ToList(),
            Guests = tour.GuestsCount,
            ChildrenCount = tour.ChildrenCount,
            Days = tour.Days,
            PreviewImageId = tour.PreviewImageId,
            Services = tour.ValuesTourAttributes.Select(val => new ServiceResponse()
            {
                Name = val.TourAttribute.Name,
                MeasureOfUnit = val.TourAttribute.MeasureUnit,
                Value = val.Value
            }).ToList(),
            Accomodation = new HotelWithAccResponse()
            {
                HotelName = tour.Accomodation.Type.Hotel.Name,
                Category = tour.Accomodation.Type.Hotel.CategoryCodeNavigation.Value,
                City = tour.Accomodation.Type.Hotel.City.Name,
                Country = tour.Accomodation.Type.Hotel.City.Country.Name,
                HotelPreviewImageId = tour.Accomodation.Type.Hotel.PreviewImageId.Value,
                AccomodationName = tour.Accomodation.Type.Name,
                Capacity = tour.Accomodation.Type.Capacity,
                HotelServices = tour.Accomodation.Type.Hotel.ValuesHotelsAttributes.Select(val => new ServiceResponse()
                {
                    Name = val.HotelAttribute.Name,
                    MeasureOfUnit = val.HotelAttribute.MeasureUnit,
                    Value = val.Value
                }).ToList(),
                AccomodationServices = tour.Accomodation.Type.ValuesAccomodationAttributes.Select(val => new ServiceResponse()
                {
                    Name = val.AccomodationAttribute.Name,
                    MeasureOfUnit = val.AccomodationAttribute.MeasureUnit,
                    Value = val.Value
                }).ToList(),
                AccomodationImages = tour.Accomodation.Type.Images.Select(img => img.Id)
            }
        };
    }
}