namespace TravelCompanyDAL.Entities;

public partial class ValuesAccomodationAttribute
{
    public int AccomodationId { get; set; }
    public int AccomodationAttributeId { get; set; }
    public string Value { get; set; } = null!;

    public virtual Accomodation Accomodation { get; set; } = null!;
    public virtual AccomodationsAttribute AccomodationAttribute { get; set; } = null!;
}