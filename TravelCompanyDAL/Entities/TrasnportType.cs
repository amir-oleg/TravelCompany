namespace TravelCompanyDAL.Entities;

public partial class TrasnportType
{
    public TrasnportType()
    {
        Ways = new HashSet<Way>();
    }

    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;

    public virtual ICollection<Way> Ways { get; set; }
}