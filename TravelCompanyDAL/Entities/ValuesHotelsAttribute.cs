namespace TravelCompanyDAL.Entities;

public partial class ValuesHotelsAttribute
{
    public int HotelId { get; set; }
    public int HotelAttributeId { get; set; }
    public string Value { get; set; } = null!;

    public virtual Hotel Hotel { get; set; } = null!;
    public virtual HotelsAttribute HotelAttribute { get; set; } = null!;
}