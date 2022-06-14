using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities;

public partial class Accomodation
{
    public Accomodation()
    {
        Occupancies = new HashSet<Occupancy>();
        Orders = new HashSet<Order>();
        Images = new HashSet<Image>();
    }

    public int Id { get; set; }
    public int Capacity { get; set; }
    public string Name { get; set; } = null!;
    public int HotelId { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsWifiExists { get; set; }
    public bool IsAcExists { get; set; }
    public bool IsWcExixts { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
    public virtual ICollection<Occupancy> Occupancies { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}