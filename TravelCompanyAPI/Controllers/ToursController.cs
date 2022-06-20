using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Models;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToursController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchToursByWay([FromQuery] int page, [FromBody] SearchToursByWayDto request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SearchToursByWayRequest
        (
            request.StartPlace,
            request.EndPlace,
            request.TransportType,
            request.GuestsCount,
            request.ChildrenCount,
            request.StartDate,
            request.Days,
            request.DietTypes,
            request.HotelCategories,
            request.TourCategories,
            request.PriceFrom,
            request.PriceTo,
            page
        ), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("tour/{id:int}")]
    public async Task<IActionResult> GetTourById(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTourByIdRequest(id), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("tour")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AddTour([FromBody] AddTourDto request,CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new AddTourRequest(request.Name, request.StartCity, request.EndCity, request.TransportType, request.GuestsCount,
                request.ChildrenCount, request.Days, request.DietType, request.TourCategories, request.Price,
                request.Attributes, request.Accomodation, request.Image), cancellationToken);
        return Ok();
    }

}