using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Manager)]
    [Route("order/{id:int}/take-in-job")]
    public async Task<IActionResult> TakeInJob(int id, CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        await _mediator.Send(new TakeOrderInJobRequest(email, id), cancellationToken);

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Manager)]
    [Route("free")]
    public async Task<IActionResult> GetFreeOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetFreeOrdersRequest(), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Manager)]
    [Route("order/{id:int}/mark-as-paid")]
    public async Task<IActionResult> MarkAsPaid(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new MarkOrderAsPaidRequest(id), cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = $"{UserRoles.Manager},{UserRoles.User}")]
    [Route("order/{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelOrderRequest(id), cancellationToken);

        return Ok();
    }
}