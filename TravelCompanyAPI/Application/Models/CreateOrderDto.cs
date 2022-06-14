using System.ComponentModel.DataAnnotations;

namespace TravelCompanyAPI.Application.Models;

public class CreateOrderDto
{
    [Required] 
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required] 
    public int AccomodationId { get; set; }
}