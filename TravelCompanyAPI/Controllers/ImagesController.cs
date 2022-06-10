using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly TravelCompanyClassicContext _context;

        public ImagesController(TravelCompanyClassicContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImage(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
                return null;
            var bytes = (await _context.Images.FirstOrDefaultAsync(img => img.Id == id, cancellationToken))?.ImageBytes;
            return File(bytes, "image/jpeg");
        }
    }
}
