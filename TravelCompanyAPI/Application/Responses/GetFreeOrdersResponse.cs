namespace TravelCompanyAPI.Application.Responses;

public class GetFreeOrdersResponse
{
    public List<ManagerOrderResponse> Orders { get; set; } = new();
}