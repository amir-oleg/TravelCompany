using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConstsController : ControllerBase
{
    private readonly TravelCompanyEAVContext _context;

    public ConstsController(TravelCompanyEAVContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("diet-types")]
    public async Task<IActionResult> GetDietTypes(CancellationToken cancellationToken)
    {
        var response = await _context.DietTypes.AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("hotel-cats")]
    public async Task<IActionResult> GetHotelCategories(CancellationToken cancellationToken)
    {
        var response = await _context.HotelCategories.AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("tour-cats")]
    public async Task<IActionResult> GetTourCategories(CancellationToken cancellationToken)
    {
        var response = await _context.TourCategories.AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("hotels/{cityId:int}")]
    public async Task<IActionResult> GetHotels(int cityId, CancellationToken cancellationToken)
    {
        var response = await _context.Hotels.Where(hotel => hotel.CityId == cityId)
            .Select(hotel => new { hotel.Id, hotel.Name }).AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("accomodations/{hotelId:int}")]
    public async Task<IActionResult> GetAccomodations(int hotelId, CancellationToken cancellationToken)
    {
        var response = await _context.AccomodationTypes.Where(acc => acc.HotelId == hotelId)
            .Select(acc => new { acc.Id, acc.Name }).AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("cities/{countryId:int}")]
    public async Task<IActionResult> GetCities(int countryId, CancellationToken cancellationToken)
    {
        if (countryId == 0)
        {
            return Ok();
        }

        var response = await _context.Cities.Where(city => city.CountryId == countryId)
            .Select(city => new { city.Id, city.Name }).OrderBy(city => city.Name)
            .AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("countries")]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
    {

        var response = await _context.Countries.Select(country => new { country.Id, country.Name })
            .OrderBy(country => country.Name).AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("transportTypes")]
    public async Task<IActionResult> GetTransportTypes(CancellationToken cancellationToken)
    {

        var response = await _context.TrasnportTypes.AsNoTracking().ToListAsync(cancellationToken);

        return Ok(response);
    }
}