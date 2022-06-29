namespace TravelCompanyAPI.Application.Responses;

public class GetPersonalResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public List<OrderResponse> Orders { get; set; } = new();
}

public class OrderResponse
{
    public int Id { get; set; }
    public string TourName { get; set; }
    public string HotelName { get; set; }
    public string AccomodationName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public decimal? Price { get; set; }
    public bool IsPaid { get; set; }
}