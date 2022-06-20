namespace TravelCompanyDAL.Entities;

public partial class ToursAttribute
{
    public ToursAttribute()
    {
        ValuesTourAttributes = new HashSet<ValuesTourAttribute>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? MeasureUnit { get; set; }

    public virtual ICollection<ValuesTourAttribute> ValuesTourAttributes { get; set; }
}