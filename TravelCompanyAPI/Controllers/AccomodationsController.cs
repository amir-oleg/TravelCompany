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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationInHotelResponse>))]
    public async Task<ActionResult<GetAccomodationsResponse>> GetAccomodations([FromQuery] string country, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int guests, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsRequest(country,startDate, endDate, guests), token);

        return result;
    }
}