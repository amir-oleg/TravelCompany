namespace TravelCompanyDAL.Entities;

public partial class Accomodation
{
    public Accomodation()
    {
        Occupancies = new HashSet<Occupancy>();
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    public int TypeId { get; set; }
    public int Number { get; set; }

    public virtual AccomodationType Type { get; set; } = null!;
    public virtual ICollection<Occupancy> Occupancies { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}