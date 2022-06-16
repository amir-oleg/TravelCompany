using MediatR;

namespace TravelCompanyAPI.Application.Commands;

public class UpdateHotelAttributeRequest: IRequest
{
    public UpdateHotelAttributeRequest(string name, string value, string measureOfUnit, int hotelId)
    {
        Name = name;
        Value = value;
        MeasureOfUnit = measureOfUnit;
        HotelId = hotelId;
    }

    public string Name { get; set; }
    public string Value { get; set; }
    public string MeasureOfUnit { get; set; }
    public int HotelId { get; set; }
}