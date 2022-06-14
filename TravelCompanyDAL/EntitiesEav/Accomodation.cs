using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.EntitiesEav;

public partial class Accomodation
{
    public Accomodation()
    {
        Occupancies = new HashSet<Occupancy>();
        Orders = new HashSet<Order>();
        ValuesAccomodationAttributes = new HashSet<ValuesAccomodationAttribute>();
        Images = new HashSet<Image>();
    }

    public int Id { get; set; }
    public int Capacity { get; set; }
    public string Name { get; set; } = null!;
    public int HotelId { get; set; }
    public decimal PricePerDay { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
    public virtual ICollection<Occupancy> Occupancies { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<ValuesAccomodationAttribute> ValuesAccomodationAttributes { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}