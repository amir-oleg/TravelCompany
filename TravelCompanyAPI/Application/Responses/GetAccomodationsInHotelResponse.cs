namespace TravelCompanyAPI.Application.Responses;

public class GetAccomodationInHotelResponse
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<string> Services { get; set; }
    public IEnumerable<int> Images { get; set; }
    
}