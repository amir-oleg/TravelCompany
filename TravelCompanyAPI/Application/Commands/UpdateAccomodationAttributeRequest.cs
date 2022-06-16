using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class UpdateAccomodationAttributeRequest: IRequest
{
    public UpdateAccomodationAttributeRequest(string name, string value, string measureOfUnit, int accomodationId)
    {
        Name = name;
        Value = value;
        MeasureOfUnit = measureOfUnit;
        AccomodationId = accomodationId;
    }

    public string Name { get; set; }
    public string Value { get; set; }
    public string MeasureOfUnit { get; set; }
    public int AccomodationId { get; set; }
}