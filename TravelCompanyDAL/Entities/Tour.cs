namespace TravelCompanyDAL.Entities;

public partial class Tour
{
    public Tour()
    {
        Orders = new HashSet<Order>();
        ValuesTourAttributes = new HashSet<ValuesTourAttribute>();
        TourCategoryCodes = new HashSet<TourCategory>();
    }

    public int Id { get; set; }
    public int WayId { get; set; }
    public string DietCode { get; set; } = null!;
    public int GuestsCount { get; set; }
    public int ChildrenCount { get; set; }
    public decimal Cost { get; set; }
    public string Name { get; set; } = null!;
    public int Days { get; set; }
    public int PreviewImageId { get; set; }
    public int AccomodationTypeId { get; set; }

    public virtual AccomodationType AccomodationType { get; set; } = null!;
    public virtual DietType DietCodeNavigation { get; set; } = null!;
    public virtual Image PreviewImage { get; set; } = null!;
    public virtual Way Way { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<ValuesTourAttribute> ValuesTourAttributes { get; set; }

    public virtual ICollection<TourCategory> TourCategoryCodes { get; set; }
}