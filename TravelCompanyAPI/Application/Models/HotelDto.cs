namespace TravelCompanyAPI.Application.Models;

public class HotelDto
{
    public int Id { get; set; }
    public decimal LowestPrice { get; set; }
    public string Name { get; set; }
    public int CountOfStars { get; set; }
    public int? PreviewImage { get; set; }
}