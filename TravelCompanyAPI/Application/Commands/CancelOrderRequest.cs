using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class CancelOrderRequest: IRequest
{
    public CancelOrderRequest(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; set; }
}