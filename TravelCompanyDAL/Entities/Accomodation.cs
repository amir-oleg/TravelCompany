using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Accomodation
    {
        public Accomodation()
        {
            Tours = new HashSet<Tour>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public int CountOfStars { get; set; }
        public int Hotel { get; set; }

        public virtual Hotel HotelNavigation { get; set; } = null!;
        public virtual ICollection<Tour> Tours { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
