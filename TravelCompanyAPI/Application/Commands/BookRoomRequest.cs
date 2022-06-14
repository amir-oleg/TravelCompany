using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class BookRoomRequest: IRequest
{
    public BookRoomRequest(string email, DateTime startDate, DateTime endDate, int accomodationId)
    {
        Email = email;
        StartDate = startDate;
        EndDate = endDate;
        AccomodationId = accomodationId;
    }

    public string Email { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AccomodationId { get; set; }
}