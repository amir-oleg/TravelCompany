namespace TravelCompanyDAL.Entities;

public partial class AccomodationsAttribute
{
    public AccomodationsAttribute()
    {
        ValuesAccomodationAttributes = new HashSet<ValuesAccomodationAttribute>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? MeasureUnit { get; set; }

    public virtual ICollection<ValuesAccomodationAttribute> ValuesAccomodationAttributes { get; set; }
}