using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyDAL.Entities;

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

    // GET: api/Accomodations
    [HttpGet]
    [Route("api/hotel/{hotelId:int}/accomodations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccomodationInHotelResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetAccomodationInHotelResponse>>> GetAccomodations(int hotelId, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationsInHotelRequest(hotelId), token);

        if (!result.Any())
        {
            return NotFound();
        }

        return result;
    }

    // GET: api/Accomodations/5
    [Route("api/hotel/{hotelId:int}/accomodation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccomodationInHotelResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAccomodationInHotelResponse>> GetAccomodation(int hotelId,int id, CancellationToken token)
    {
        var result = await _mediator.Send(new GetAccomodationRequest(hotelId, id), token);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    // PUT: api/Accomodations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccomodation(int id, Accomodation accomodation)
    {
        throw new NotImplementedException();
    }

    // POST: api/Accomodations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Accomodation>> PostAccomodation(Accomodation accomodation)
    {
        throw new NotImplementedException();
    }

    // DELETE: api/Accomodations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccomodation(int id)
    {
        throw new NotImplementedException();
    }

    private bool AccomodationExists(int id)
    {
        throw new NotImplementedException();
    }
}