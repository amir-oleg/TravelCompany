namespace TravelCompanyDAL.Entities
{
    public partial class ValuesToursAttribute
    {
        public int TourId { get; set; }
        public int TourAttributeId { get; set; }
        public string Value { get; set; } = null!;

        public virtual Tour Tour { get; set; } = null!;
        public virtual ToursAttribute TourAttribute { get; set; } = null!;
    }
}
