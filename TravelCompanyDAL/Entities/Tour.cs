using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities;

public partial class Tour
{
    public Tour()
    {
        Orders = new HashSet<Order>();
        Services = new HashSet<Service>();
    }

    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Type { get; set; } = null!;
    public string TypeOfDiet { get; set; } = null!;
    public int Accomodation { get; set; }
    public int StartTrip { get; set; }
    public int EndTrip { get; set; }

    public virtual Accomodation AccomodationNavigation { get; set; } = null!;
    public virtual Trip EndTripNavigation { get; set; } = null!;
    public virtual Trip StartTripNavigation { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; }

    public virtual ICollection<Service> Services { get; set; }
}