using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class TakeOrderInJobRequest: IRequest
{
    public TakeOrderInJobRequest(string email, int orderId)
    {
        Email = email;
        OrderId = orderId;
    }

    public string Email { get; set; }
    public int OrderId { get; set; }
}