﻿using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.EntitiesEav;

public partial class Image
{
    public Image()
    {
        Hotels = new HashSet<Hotel>();
        Accomodations = new HashSet<Accomodation>();
    }

    public int Id { get; set; }
    public byte[] ImageBytes { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; }

    public virtual ICollection<Accomodation> Accomodations { get; set; }
}