using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Occupancy
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Accomodation { get; set; }

        public virtual Accomodation AccomodationNavigation { get; set; } = null!;
    }
}
