using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Accomodation
    {
        public Accomodation()
        {
            Occupancies = new HashSet<Occupancy>();
            Tours = new HashSet<Tour>();
            Images = new HashSet<Image>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public int Hotel { get; set; }
        public decimal PricePerDay { get; set; }

        public virtual Hotel HotelNavigation { get; set; } = null!;
        public virtual ICollection<Occupancy> Occupancies { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
