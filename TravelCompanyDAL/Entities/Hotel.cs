using System;
using System.Collections.Generic;

namespace TravelCompanyDAL.Entities;

public partial class Hotel
{
    public Hotel()
    {
        Accomodations = new HashSet<Accomodation>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountOfStars { get; set; }
    public int CityId { get; set; }
    public string TypeOfAccommodation { get; set; } = null!;
    public int? PreviewImageId { get; set; }
    public string? TypeOfDiet { get; set; }
    public DateTime? DateOfFoundation { get; set; }
    public int? MetrsToBeach { get; set; }

    public virtual City City { get; set; } = null!;
    public virtual Image? PreviewImage { get; set; }
    public virtual ICollection<Accomodation> Accomodations { get; set; }
}