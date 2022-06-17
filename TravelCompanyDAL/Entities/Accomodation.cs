namespace TravelCompanyDAL.Entities
{
    public partial class Accomodation
    {
        public Accomodation()
        {
            Occupancies = new HashSet<Occupancy>();
            ValuesAccomodationAttributes = new HashSet<ValuesAccomodationAttribute>();
            Images = new HashSet<Image>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; } = null!;
        public int HotelId { get; set; }
        public decimal PricePerDay { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<Occupancy> Occupancies { get; set; }
        public virtual ICollection<ValuesAccomodationAttribute> ValuesAccomodationAttributes { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
