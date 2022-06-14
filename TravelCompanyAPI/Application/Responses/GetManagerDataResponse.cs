namespace TravelCompanyAPI.Application.Responses;

public class GetManagerDataResponse
{
 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public List<ManagerOrderResponse> Orders { get; set; } = new();
}

public class ManagerOrderResponse
{
    public int Id { get; set; }
    public string HotelName { get; set; }
    public string AccomodationName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public decimal Price { get; set; }
    public string ClientName { get; set; }
    public string ClientPhoneNumber { get; set; }
    public bool IsPaid { get; set; }
}