namespace TravelCompanyDAL.Entities;

public partial class Accomodation
{
    public Accomodation()
    {
        Occupancies = new HashSet<Occupancy>();
        Tours = new HashSet<Tour>();
    }

    public int Id { get; set; }
    public int TypeId { get; set; }

    public virtual AccomodationType Type { get; set; } = null!;
    public virtual ICollection<Occupancy> Occupancies { get; set; }
    public virtual ICollection<Tour> Tours { get; set; }
}