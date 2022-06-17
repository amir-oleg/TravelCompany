using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Controllers;

[Route("api/lk")]
[ApiController]
[Authorize]
public class PersonalCabinetController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonalCabinetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> GetPersonalData(CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var response = await _mediator.Send(new GetPersonalDataRequest(email), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = UserRoles.Manager)]
    public async Task<IActionResult> GetManagerData(CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var response = await _mediator.Send(new GetManagerDataRequest(email), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("manager/new-orders")]
    [Authorize(Roles = UserRoles.Manager)]
    public async Task<IActionResult> GetNewOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetNewOrdersRequest(), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetManagersStatistics(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetManagersStatisticsRequest(), cancellationToken);

        return Ok(response);
    }
}