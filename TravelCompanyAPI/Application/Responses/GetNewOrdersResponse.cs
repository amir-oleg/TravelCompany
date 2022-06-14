namespace TravelCompanyAPI.Application.Responses;

public class GetNewOrdersResponse
{
    public List<ManagerOrderResponse> Orders { get; set; } = new();
}