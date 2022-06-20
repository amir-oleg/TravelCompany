namespace TravelCompanyDAL.Entities;

public partial class TourCategory
{
    public TourCategory()
    {
        Tours = new HashSet<Tour>();
    }

    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; }
}