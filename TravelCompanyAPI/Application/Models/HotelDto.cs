namespace TravelCompanyAPI.Application.Models;

public class HotelDto
{
    public int Id { get; set; }
    public decimal LowestPrice { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Category { get; set; }
    public int? PreviewImage { get; set; }
}