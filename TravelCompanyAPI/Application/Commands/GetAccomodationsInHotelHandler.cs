using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL;

namespace TravelCompanyAPI.Application.Commands
{
    internal class GetAccomodationsInHotelHandler: IRequestHandler<GetAccomodationsInHotelRequest, List<GetAccomodationInHotelResponse>>
    {
        private readonly TravelCompanyClassicContext _context;

        public GetAccomodationsInHotelHandler(TravelCompanyClassicContext context)
        {
            _context = context;
        }

        public async Task<List<GetAccomodationInHotelResponse>> Handle(GetAccomodationsInHotelRequest request, CancellationToken cancellationToken)
        {
            var accomodations = await _context.Accomodations
                .Where(accomodation => accomodation.Hotel == request.HotelId)
                .Include(accomodation => accomodation.Services)
                .ToListAsync(cancellationToken);

            return accomodations.Select(accomodation => new GetAccomodationInHotelResponse
            {
                Capacity = accomodation.Capacity, CountOfStars = accomodation.CountOfStars,
                Services = accomodation.Services.Select(services => services.Name).ToList()
            }).ToList();
        }
    }
}
