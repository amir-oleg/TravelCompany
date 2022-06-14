using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class MarkOrderAsPaidRequest: IRequest
{
    public MarkOrderAsPaidRequest(int orderId)
    {
        OrderId = orderId;
    }
    public int OrderId { get; set; }
}