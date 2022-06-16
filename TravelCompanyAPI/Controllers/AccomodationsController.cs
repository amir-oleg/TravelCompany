using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL.EntitiesEav;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationsInHotelResponse>))]
    public async Task<ActionResult<GetAccomodationsResponse>> GetAccomodations([FromQuery] string country, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int guests, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsRequest(country,startDate, endDate, guests), token);

        return result;
    }

    [HttpPost]
    [Route("{id:int}/attribute")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UpdateAccomodationAttribute(int id, [FromBody] UpdateAttributeDto request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateAccomodationAttributeRequest(request.Name, request.Value, request.MeasureOfUnit, id),
            cancellationToken);

        return Ok();
    }
}