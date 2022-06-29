namespace TravelCompanyAPI.Application.Responses;

public class GetAccomodationsInHotelEavResponse
{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerDay{ get; set;}
        public List<ServiceResponse> Attributes { get; set; } = new();
        public IEnumerable<string> Images { get; set; }
}