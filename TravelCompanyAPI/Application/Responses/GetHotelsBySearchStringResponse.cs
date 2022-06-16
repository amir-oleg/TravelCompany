namespace TravelCompanyAPI.Application.Responses;

public class GetHotelsBySearchStringResponse
{
    public List<HotelsBySearchString> Hotels { get; set; }
    public int PageCount { get; set; }
}

public class HotelsBySearchString
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? PreviewImage { get; set; }
}