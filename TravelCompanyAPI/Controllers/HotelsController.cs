using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;
using TravelCompanyDAL.Entities;

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
    [Route("{hotelId:int}/accomodations/eav")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationsInHotelEavResponse>))]
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

    [HttpGet]
    [Route("{hotelId:int}/accomodations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationsInHotelResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetAccomodationsInHotelResponse>>> GetAccomodations(int hotelId, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsInHotelRequest(hotelId), token);

        if (!result.Any())
        {
            return NotFound();
        }

        return result;
    }


    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetHotelsBySearchString([FromQuery] string search, [FromQuery] int page, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(search))
           return BadRequest();

        var response = await _mediator.Send(new GetHotelsBySearchStringRequest(search, page), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("{id:int}/attribute")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UpdateHotelAttribute(int id, [FromBody] UpdateAttributeDto request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateHotelAttributeRequest(request.Name, request.Value, request.MeasureOfUnit, id), cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Route("add")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AddHotel([FromBody] AddHotelDto request, CancellationToken cancellationToken)
    {
        var imageBytes = Convert.FromBase64String(request.PreviewImageBase64);
        await _mediator.Send(
            new AddHotelRequest(request.HotelName, request.CategoreCode, request.City, request.Country,
                request.TypeOfAccommodation, imageBytes), cancellationToken);

        return Ok();
    }
}