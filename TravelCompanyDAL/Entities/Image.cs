namespace TravelCompanyDAL.Entities;

public partial class Image
{
    public Image()
    {
        Hotels = new HashSet<Hotel>();
        Tours = new HashSet<Tour>();
        Accomodations = new HashSet<AccomodationType>();
    }

    public int Id { get; set; }
    public byte[] ImageBytes { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; }
    public virtual ICollection<Tour> Tours { get; set; }

    public virtual ICollection<AccomodationType> Accomodations { get; set; }
}