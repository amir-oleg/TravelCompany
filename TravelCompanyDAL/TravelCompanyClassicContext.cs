using Microsoft.EntityFrameworkCore;
using TravelCompanyDAL.Entities;

namespace TravelCompanyDAL;

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
    public virtual DbSet<Image> Images { get; set; } = null!;
    public virtual DbSet<Occupancy> Occupancies { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accomodation>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.Property(e => e.PricePerDay).HasColumnType("money");

            entity.HasOne(d => d.Hotel)
                .WithMany(p => p.Accomodations)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accomodations_Hotels");

            entity.HasMany(d => d.Images)
                .WithMany(p => p.Accomodations)
                .UsingEntity<Dictionary<string, object>>(
                    "AccomodationImage",
                    l => l.HasOne<Image>().WithMany().HasForeignKey("ImageId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Accomodation_Images_Images"),
                    r => r.HasOne<Accomodation>().WithMany().HasForeignKey("AccomodationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Accomodation_Images_Accomodations"),
                    j =>
                    {
                        j.HasKey("AccomodationId", "ImageId");

                        j.ToTable("Accomodation_Images");
                    });
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Countries");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Address).IsUnicode(false);

            entity.Property(e => e.BirthDate).HasColumnType("date");

            entity.Property(e => e.Email).HasMaxLength(255);

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
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsFixedLength();

            entity.Property(e => e.BirthDate).HasColumnType("date");

            entity.Property(e => e.ContactPhone).HasMaxLength(20);

            entity.Property(e => e.Email).HasMaxLength(255);

            entity.Property(e => e.FirstName).HasMaxLength(100);

            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.Property(e => e.Patronymic).HasMaxLength(100);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(e => e.DateOfFoundation).HasColumnType("date");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.TypeOfAccommodation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.TypeOfDiet).HasMaxLength(255);

            entity.HasOne(d => d.City)
                .WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hotels_Cities");

            entity.HasOne(d => d.PreviewImage)
                .WithMany(p => p.Hotels)
                .HasForeignKey(d => d.PreviewImageId)
                .HasConstraintName("FK_Hotels_Images");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.ImageBytes).HasColumnType("image");
        });

        modelBuilder.Entity<Occupancy>(entity =>
        {
            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Accomodation)
                .WithMany(p => p.Occupancies)
                .HasForeignKey(d => d.AccomodationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Occupancies_Accomodations");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Cost).HasColumnType("money");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Accomodation)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccomodationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Accomodations");

            entity.HasOne(d => d.Client)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Clients");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}