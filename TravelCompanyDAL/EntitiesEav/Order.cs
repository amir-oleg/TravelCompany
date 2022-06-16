﻿using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.EntitiesEav;

public partial class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Date { get; set; }
    public decimal Cost { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsPaid { get; set; }
    public int AccomodationId { get; set; }

    public virtual Accomodation Accomodation { get; set; } = null!;
    public virtual Client Client { get; set; } = null!;
    public virtual Employee? Employee { get; set; }
}