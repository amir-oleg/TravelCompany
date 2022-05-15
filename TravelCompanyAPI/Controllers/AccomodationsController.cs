using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelCompanyAPI.Application.Commands;
using TravelCompanyDAL;
using TravelCompanyDAL.Entities;

namespace TravelCompanyAPI.Controllers
{
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
            if (id != accomodation.Id)
            {
                return BadRequest();
            }

            _context.Entry(accomodation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accomodations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accomodation>> PostAccomodation(Accomodation accomodation)
        {
          if (_context.Accomodations == null)
          {
              return Problem("Entity set 'TravelCompanyClassicContext.Accomodations'  is null.");
          }
            _context.Accomodations.Add(accomodation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccomodationExists(accomodation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccomodation", new { id = accomodation.Id }, accomodation);
        }

        // DELETE: api/Accomodations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccomodation(int id)
        {
            if (_context.Accomodations == null)
            {
                return NotFound();
            }
            var accomodation = await _context.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            _context.Accomodations.Remove(accomodation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccomodationExists(int id)
        {
            return (_context.Accomodations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
