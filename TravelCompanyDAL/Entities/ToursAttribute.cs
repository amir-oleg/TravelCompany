namespace TravelCompanyDAL.Entities
{
    public partial class ToursAttribute
    {
        public ToursAttribute()
        {
            ValuesToursAttributes = new HashSet<ValuesToursAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? MeasureUnit { get; set; }

        public virtual ICollection<ValuesToursAttribute> ValuesToursAttributes { get; set; }
    }
}
