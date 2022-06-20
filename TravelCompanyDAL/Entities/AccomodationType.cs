namespace TravelCompanyDAL.Entities;

public partial class AccomodationType
{
    public AccomodationType()
    {
        Accomodations = new HashSet<Accomodation>();
        ValuesAccomodationAttributes = new HashSet<ValuesAccomodationAttribute>();
        Images = new HashSet<Image>();
    }

    public int Id { get; set; }
    public int Capacity { get; set; }
    public string Name { get; set; } = null!;
    public int HotelId { get; set; }
    public decimal PricePerDay { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
    public virtual ICollection<Accomodation> Accomodations { get; set; }
    public virtual ICollection<ValuesAccomodationAttribute> ValuesAccomodationAttributes { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}