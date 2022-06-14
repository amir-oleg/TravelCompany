namespace TravelCompanyDAL.Entities;

public partial class Client
{
    public Client()
    {
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; }
}