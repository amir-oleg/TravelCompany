using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Trip
    {
        public Trip()
        {
            TourEndTripNavigations = new HashSet<Tour>();
            TourStartTripNavigations = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public int StartCity { get; set; }
        public int EndCity { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual ICollection<Tour> TourEndTripNavigations { get; set; }
        public virtual ICollection<Tour> TourStartTripNavigations { get; set; }
    }
}
