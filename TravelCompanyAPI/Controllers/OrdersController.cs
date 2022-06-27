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

        response = new GetFreeOrdersResponse()
        {
            Orders = new List<ManagerOrderResponse>()
            {
                new ManagerOrderResponse()
                {
                    ClientName = "Петров Петр Петрович",
                    ClientPhoneNumber = "+795865412132",
                    StartDate = "21-06-2022",
                    EndDate = "30-06-2022",
                    Id = 32,
                    IsPaid = false,
                    Price = 154212,
                    TourName = "Анубис",
                    HotelName = "SHARMING INN HOTEL",
                    AccomodationName = "Standart"
                },
                new ManagerOrderResponse()
                {
                    ClientName = "Иванов Иван Иванович",
                    ClientPhoneNumber = "+79645487898",
                    StartDate = "30-06-2022",
                    EndDate = "07-07-2022",
                    Id = 37,
                    IsPaid = false,
                    Price = 155678,
                    TourName = "Анубис",
                    HotelName = "SHARMING INN HOTEL",
                    AccomodationName = "Standart"
                },
                new ManagerOrderResponse()
                {
                    ClientName = "Говорун Петр Владимирович",
                    ClientPhoneNumber = "+795878514132",
                    StartDate = "23-06-2022",
                    EndDate = "30-06-2022",
                    Id = 38,
                    IsPaid = false,
                    Price = 60584,
                    TourName = "Волга",
                    HotelName = "Саратов",
                    AccomodationName = "Стандартный"
                },
                new ManagerOrderResponse()
                {
                    ClientName = "Рябцрв Иван Романович",
                    ClientPhoneNumber = "+79678547898",
                    StartDate = "30-06-2022",
                    EndDate = "07-07-2022",
                    Id = 45,
                    IsPaid = false,
                    Price = 126876,
                    TourName = "Пирамиды египта",
                    HotelName = "SHARMING INN HOTEL",
                    AccomodationName = "Standart"
                }
            }
        };

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

    [HttpPost]
    [Authorize(Roles = $"{UserRoles.User}")]
    [Route("order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto request, CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        await _mediator.Send(new CreateOrderRequest(request.StartDate, request.EndDate, request.AccomodationId, request.TourId, email), cancellationToken);

        return Ok();
    }
}