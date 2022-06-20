namespace TravelCompanyDAL.Entities;

public partial class HotelCategory
{
    public HotelCategory()
    {
        Hotels = new HashSet<Hotel>();
    }

    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; }
}