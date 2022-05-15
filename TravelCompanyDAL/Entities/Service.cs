using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Service
    {
        public Service()
        {
            Accomadations = new HashSet<Accomodation>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Accomodation> Accomadations { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
