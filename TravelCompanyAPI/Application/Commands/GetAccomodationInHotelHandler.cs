using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands
{
    public class GetAccomodationInHotelHandler: IRequestHandler<GetAccomodationRequest, GetAccomodationInHotelResponse>
    {
        private readonly TravelCompanyClassicContext _context;

        public GetAccomodationInHotelHandler(TravelCompanyClassicContext context)
        {
            _context = context;
        }

        public async Task<GetAccomodationInHotelResponse> Handle(GetAccomodationRequest request, CancellationToken cancellationToken)
        {
            var accomodation = await _context.Accomodations
                .Where(accomodation => accomodation.Hotel == request.HotelId && accomodation.Id == request.AccomodationId)
                .Include(accomodation => accomodation.Services)
                .FirstOrDefaultAsync(cancellationToken);

            return accomodation == null
                ? null
                : new GetAccomodationInHotelResponse
                {
                    Capacity = accomodation.Capacity,
                    CountOfStars = accomodation.CountOfStars,
                    Services = accomodation.Services.Select(services => services.Name).ToList()
                };
        }
    }
}
