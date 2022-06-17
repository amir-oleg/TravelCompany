namespace TravelCompanyDAL.Entities
{
    public partial class City
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
            WayEndCities = new HashSet<Way>();
            WayStartCities = new HashSet<Way>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Way> WayEndCities { get; set; }
        public virtual ICollection<Way> WayStartCities { get; set; }
    }
}
