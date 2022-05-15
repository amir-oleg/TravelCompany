namespace TravelCompanyAPI.Application.Commands
{
    public class GetAccomodationInHotelResponse
    {
        public int Capacity { get; set; }
        public int CountOfStars { get; set; }
        public List<string> Services { get; set; }
    }
}
