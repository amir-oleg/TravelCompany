using System.ComponentModel.DataAnnotations;

namespace TravelCompanyAPI.Application.Models;

public class RegisterModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Phone]
    [Required(ErrorMessage = "Phone number is required")]
    public string PhoneNumber { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Patronymic { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}