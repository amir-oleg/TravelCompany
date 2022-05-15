using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL.Entities;

namespace TravelCompanyDAL
{
    public partial class TravelCompanyClassicContext : DbContext
    {
        public TravelCompanyClassicContext()
        {
        }

        public TravelCompanyClassicContext(DbContextOptions<TravelCompanyClassicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accomodation> Accomodations { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=BULL-PC\\SQLEXPRESS;Database=TravelCompanyClassic;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accomodation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.HotelNavigation)
                    .WithMany(p => p.Accomodations)
                    .HasForeignKey(d => d.Hotel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accomodations_Hotels");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ContactPhone).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Patronymic).HasMaxLength(100);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfAccommodation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.City)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotels_Cities");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Clients");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Tours");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasMany(d => d.Accomadations)
                    .WithMany(p => p.Services)
                    .UsingEntity<Dictionary<string, object>>(
                        "ServicesAccomodation",
                        l => l.HasOne<Accomodation>().WithMany().HasForeignKey("Accomadation").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Services_Accomodations_Accomodations"),
                        r => r.HasOne<Service>().WithMany().HasForeignKey("Service").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Services_Accomodations_Services"),
                        j =>
                        {
                            j.HasKey("Service", "Accomadation");

                            j.ToTable("Services_Accomodations");
                        });

                entity.HasMany(d => d.Tours)
                    .WithMany(p => p.Services)
                    .UsingEntity<Dictionary<string, object>>(
                        "ServicesTour",
                        l => l.HasOne<Tour>().WithMany().HasForeignKey("Tour").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Services_Tours_Tours"),
                        r => r.HasOne<Service>().WithMany().HasForeignKey("Service").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Services_Tours_Services"),
                        j =>
                        {
                            j.HasKey("Service", "Tour");

                            j.ToTable("Services_Tours");
                        });
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfDiet)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccomodationNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.Accomodation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_Accomodations");

                entity.HasOne(d => d.EndTripNavigation)
                    .WithMany(p => p.TourEndTripNavigations)
                    .HasForeignKey(d => d.EndTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_Trips1");

                entity.HasOne(d => d.StartTripNavigation)
                    .WithMany(p => p.TourStartTripNavigations)
                    .HasForeignKey(d => d.StartTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_Trips");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
