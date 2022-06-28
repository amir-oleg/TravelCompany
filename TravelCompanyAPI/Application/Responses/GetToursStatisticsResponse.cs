namespace TravelCompanyAPI.Application.Responses;

public class GetToursStatisticsResponse
{
    public string TourName { get; set; }
    public int CountOfOrders { get; set; }
    public decimal Total { get; set; }
}