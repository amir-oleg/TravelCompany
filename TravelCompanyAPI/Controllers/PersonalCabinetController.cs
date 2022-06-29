using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyAPI.Application.Models;
using TravelCompanyDAL.Entities;
using TravelCompanyDAL;
using Microsoft.EntityFrameworkCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace TravelCompanyAPI.Controllers;

[Route("api/lk")]
[ApiController]
[Authorize]
public class PersonalCabinetController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly TravelCompanyEAVContext _context;

    public PersonalCabinetController(IMediator mediator, TravelCompanyEAVContext context)
    {
        _mediator = mediator;
        _context = context;
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
    [Route("doc")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> GetDoc([FromQuery] int id, CancellationToken cancellationToken)
    {
        var email = User.Claims.Single(cl => cl.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var client = await _context.Clients.FirstAsync(cl => cl.Email.ToLower() == email.ToLower(), cancellationToken);
        var order = await _context.Orders.Include(ord=> ord.Tour).ThenInclude(tour => tour.AccomodationType).ThenInclude(act => act.Hotel).FirstAsync(ord => ord.Id == id, cancellationToken);
        var html = $@"<!DOCTYPE html>
< html >
< head >
< title > Page Title </ title >
   </ head >
   < body >
   

   < h1 > {client.FirstName} {client.LastName} {client.Patronymic} </ h1 >
   <h2> {order.Tour.Name}</h2>
<h2> {order.Tour.AccomodationType.Hotel.Name}</h2>
<h2> {order.Tour.AccomodationType.Name}</h2>
<h2> {order.Tour.Cost} р.</h2>
      

      </ body >
      </ html > ";
        Byte[] res = null;
        MemoryStream ms = new MemoryStream();
        
            var pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            pdf.Save(ms);
            res = ms.ToArray();
        //};
        Response.ContentType = "text/plain";


        Response.Headers.Add("Content-Disposition", "attachment; filename=file.pdf");
        
        return File(res, "text/plain", "");
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

    [HttpPost]
    [Route("admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetManagersStatistics([FromBody] StatsDto statsDto, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetManagersStatisticsRequest(statsDto.Month), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("admin/tours")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetToursStatistics([FromBody] StatsDto statsDto, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetToursStatisticsRequest(statsDto.Month), cancellationToken);
        
        return Ok(response);
    }
}