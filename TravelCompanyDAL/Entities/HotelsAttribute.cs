namespace TravelCompanyDAL.Entities;

public partial class HotelsAttribute
{
    public HotelsAttribute()
    {
        ValuesHotelsAttributes = new HashSet<ValuesHotelsAttribute>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? MeasureUnit { get; set; }

    public virtual ICollection<ValuesHotelsAttribute> ValuesHotelsAttributes { get; set; }
}