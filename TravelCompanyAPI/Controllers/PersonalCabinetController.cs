using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Responses;
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

        response.Orders = new List<OrderResponse>()
        {
            new OrderResponse()
            {
                TourName = "Пирамиды Египта",
                StartDate = "08-08-2022",
                EndDate = "17-08-2022",
                Price = 125789,
                Id = 55,
                IsPaid = false,
            },
            new OrderResponse()
            {
                TourName = "Райские Мальдивы",
                StartDate = "07-07-2021",
                EndDate = "19-07-2021",
                Price = 220534,
                Id = 25,
                IsPaid = true,
            },
            new OrderResponse()
            {
                TourName = "Волга",
                StartDate = "03-08-2020",
                EndDate = "10-08-2020",
                Price = 55000,
                Id = 13,
                IsPaid = true,
            }
        };

        return Ok(response);
    }

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = UserRoles.Manager)]
    public async Task<IActionResult> GetManagerData(CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var response = await _mediator.Send(new GetManagerDataRequest(email), cancellationToken);
        response.Orders = new List<ManagerOrderResponse>()
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
                IsPaid = true,
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
        };
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

        response = new List<GetManagersStatisticsResponse>()
        {
            new GetManagersStatisticsResponse()
            {
                ManagerName = "Петров Петр Петрович",
                Income = 2576589
            },
            new GetManagersStatisticsResponse()
            {
                ManagerName = "Иванов Иван Петрович",
                Income = 1978543
            },
            new GetManagersStatisticsResponse()
            {
                ManagerName = "Нефедова мария Олеговна",
                Income = 1550123
            },new GetManagersStatisticsResponse()
            {
                ManagerName = "Рябцова Юлия Ивановна",
                Income = 1245789
            },new GetManagersStatisticsResponse()
            {
                ManagerName = "Грибоедова Наталия Романовна",
                Income = 754567
            },new GetManagersStatisticsResponse()
            {
                ManagerName = "Григорьев Петр Георгиевич",
                Income = 276895
            }
        };

        return Ok(response);
    }

    [HttpGet]
    [Route("admin/tours")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetToursStatistics(CancellationToken cancellationToken)
    {
        //var response = await _mediator.Send(new GetManagersStatisticsRequest(), cancellationToken);
        var response = new[] { new
        {
            TourName = "Анубис",
            CountOfOrders = 15,
            Total = 1743564,
        },
            new
            {
            TourName = "Пирамиды Египта",
            CountOfOrders = 12,
            Total = 1343564,
            },
            new
            {
                TourName = "Райские Мальдивы",
                CountOfOrders = 5,
                Total = 953564,
            },
            new
            {
                TourName = "Волга",
                CountOfOrders = 9,
                Total = 434532,
            },
            new
            {
                TourName = "Байкал",
                CountOfOrders = 2,
                Total = 235654,
            }
        };
        return Ok(response);
    }
}