using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Models;

public class AddTourDto
{
    public string Name { get; set; }
    public int StartCity { get; set; }
    public int EndCity { get; set; }
    public string TransportType { get; set; }
    public int GuestsCount { get; set; }
    public int ChildrenCount { get; set; }
    public int Days { get; set; }
    public string DietType { get; set; }
    public string[] TourCategories { get; set; }
    public decimal Price { get; set; }
    public ServiceResponse[] Attributes { get; set; }
    public int Accomodation { get; set; }
    public string Image { get; set; }
}

