namespace TravelCompanyDAL.Entities
{
    public partial class Way
    {
        public Way()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public int StartCityId { get; set; }
        public int EndCityId { get; set; }
        public string TransportType { get; set; } = null!;

        public virtual City EndCity { get; set; } = null!;
        public virtual City StartCity { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
