using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class CreateOrderRequest: IRequest
{
    public CreateOrderRequest(DateTime startDate, DateTime endDate, int accomodationId, int tourId, string userEmail)
    {
        StartDate = startDate;
        EndDate = endDate;
        AccomodationId = accomodationId;
        TourId = tourId;
        UserEmail = userEmail;
    }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AccomodationId { get; set; }
    public int TourId { get; set; }
    public string UserEmail { get; set; }
}