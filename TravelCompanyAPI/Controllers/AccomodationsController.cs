using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccomodationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccomodationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[Route("api/accomodations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationInHotelResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAccomodationsResponse>> GetAccomodations([FromQuery] string country, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int guests, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsRequest(country,startDate, endDate, guests), token);

        //if (!result.Any())
        //{
        //    return NotFound();
        //}

        return result;
    }

    [Route("api/hotel/{hotelId:int}/accomodation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccomodationInHotelResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAccomodationInHotelResponse>> GetAccomodation(int hotelId,int id, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationRequest(hotelId, id), token);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
}