using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            Accomodations = new HashSet<Accomodation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountOfStars { get; set; }
        public int City { get; set; }
        public string TypeOfAccommodation { get; set; } = null!;
        public int? PreviewImage { get; set; }

        public virtual City CityNavigation { get; set; } = null!;
        public virtual Image? PreviewImageNavigation { get; set; }
        public virtual ICollection<Accomodation> Accomodations { get; set; }
    }
}
