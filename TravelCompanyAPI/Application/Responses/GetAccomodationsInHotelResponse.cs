namespace TravelCompanyAPI.Application.Responses;

public class GetAccomodationInHotelResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public bool IsAcExists { get; set; }
    public bool IsWcExixts { get; set; }
    public bool IsWifiExists { get; set; }
    public IEnumerable<int> Images { get; set; }
    
}