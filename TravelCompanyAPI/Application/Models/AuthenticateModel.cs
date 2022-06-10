using System.ComponentModel.DataAnnotations;

namespace TravelCompanyAPI.Application.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
