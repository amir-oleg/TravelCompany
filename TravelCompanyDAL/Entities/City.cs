using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities;

public partial class City
{
    public City()
    {
        Hotels = new HashSet<Hotel>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
    public virtual ICollection<Hotel> Hotels { get; set; }
}