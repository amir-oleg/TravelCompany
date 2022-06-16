namespace TravelCompanyAPI.Application.Responses;

public class GetAccomodationsInHotelResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public List<ServiceResponse> Services { get; set; } = new();
    public IEnumerable<int> Images { get; set; }
}