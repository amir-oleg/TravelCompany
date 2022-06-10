namespace TravelCompanyAPI.Application.Models;

public class AccomodationDto
{
    public decimal PricePerDay { get; set; }
    public int Capacity { get; set; }
    public List<ServiceDto> Services { get; set; } = new();
}