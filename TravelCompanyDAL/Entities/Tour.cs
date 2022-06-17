namespace TravelCompanyDAL.Entities
{
    public partial class Tour
    {
        public Tour()
        {
            Orders = new HashSet<Order>();
            ValuesToursAttributes = new HashSet<ValuesToursAttribute>();
            Accomodations = new HashSet<Accomodation>();
            TourCategoryCodes = new HashSet<TourCategory>();
            Ways = new HashSet<Way>();
        }

        public int Id { get; set; }
        public int WayId { get; set; }
        public string DietCode { get; set; } = null!;
        public int GuestsCount { get; set; }
        public int? ChildrenCount { get; set; }
        public string CategoryCode { get; set; } = null!;
        public decimal Cost { get; set; }
        public string Name { get; set; } = null!;

        public virtual DietType DietCodeNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ValuesToursAttribute> ValuesToursAttributes { get; set; }

        public virtual ICollection<Accomodation> Accomodations { get; set; }
        public virtual ICollection<TourCategory> TourCategoryCodes { get; set; }
        public virtual ICollection<Way> Ways { get; set; }
    }
}
