using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.EntitiesEav;

public partial class AccomodationsAttribute
{
    public AccomodationsAttribute()
    {
        ValuesAccomodationAttributes = new HashSet<ValuesAccomodationAttribute>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string MeasureUnit { get; set; } = null!;

    public virtual ICollection<ValuesAccomodationAttribute> ValuesAccomodationAttributes { get; set; }
}