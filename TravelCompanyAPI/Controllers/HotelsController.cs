using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetHotelResponse>> GetHotel(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetHotelRequest(id), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet]
    [Route("{id:int}/eav")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetHotelEavResponse>> GetHotelEav(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetHotelEavRequest(id), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet]
    [Route("{hotelId:int}/accomodations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationInHotelResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetAccomodationInHotelResponse>>> GetAccomodations(int hotelId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int guests, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsInHotelRequest(hotelId, startDate, endDate, guests), token);

        if (!result.Any())
        {
            return NotFound();
        }

        return result;
    }

    [HttpGet]
    [Route("{hotelId:int}/accomodations/eav")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationInHotelResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetAccomodationsInHotelEavResponse>>> GetAccomodationsEav(int hotelId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int guests, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsInHotelEavRequest(hotelId, startDate, endDate, guests), token);

        if (!result.Any())
        {
            return NotFound();
        }

        return result;
    }
}