namespace TravelCompanyAPI.Application.Responses;

public class SearchToursByWayResponse
{
    public List<TourResponse> Tours { get; set; }
    public int PageCount { get; set; }
}

public class TourResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string DietType { get; set; }
    public int PreviewImageId { get; set; }
}