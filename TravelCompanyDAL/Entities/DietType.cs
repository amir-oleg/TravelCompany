namespace TravelCompanyDAL.Entities
{
    public partial class DietType
    {
        public DietType()
        {
            Tours = new HashSet<Tour>();
        }

        public string Code { get; set; } = null!;
        public string Value { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
