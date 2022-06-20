namespace TravelCompanyDAL.Entities;

public partial class Employee
{
    public Employee()
    {
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public DateTime BirthDate { get; set; }
    public string Position { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? Email { get; set; }
    public string ContactPhone { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; }
}