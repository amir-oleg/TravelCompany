using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int TourId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
    }
}
