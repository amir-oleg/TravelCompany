using MediatR;
using TravelCompanyAPI.Application.Models;
using TravelCompanyAPI.Application.Responses;

namespace TravelCompanyAPI.Application.Commands;

public class EditHotelRequest : IRequest
{
    public EditHotelRequest(int id, string hotelName, string categoryCode, string city, string previewImageBytes, ServiceResponse[] hotelAttributes, EditAccomodation[] accomodations)
    {
        Id = id;
        HotelName = hotelName;
        CategoryCode = categoryCode;
        City = city;
        if (previewImageBytes.Length > 10)
        { PreviewImageBytes = Convert.FromBase64String(previewImageBytes); }
        HotelAttributes = hotelAttributes.Select(att => new ServiceResponse()
        {
            Name = att.Name.Trim(),
            Value = att.Value.Trim(),
            MeasureOfUnit = att.MeasureOfUnit.Trim()
        }).ToArray();
        Accomodations = accomodations.Select(acc => new EditAccomodationRequest
        {
            Id = acc.Id,
            Name = acc.Name,
            Capacity = acc.Capacity,
            PricePerDay = acc.PricePerDay,
            
            Images = acc.Images.Where(img => img.Length > 10).Select(Convert.FromBase64String).ToList(),
            Attributes = acc.Attributes.Select(att => new ServiceResponse()
            {
                Name = att.Name.Trim(),
                Value = att.Value.Trim(),
                MeasureOfUnit = att.MeasureOfUnit.Trim()
            }).ToArray(),
            Count = acc.Count
        }).ToArray();
    }
    public int Id { get; set; }
    public string HotelName { get; set; }
    public string CategoryCode { get; set; }
    public string City { get; set; }
    public byte[] PreviewImageBytes { get; set; }
    public ServiceResponse[] HotelAttributes { get; set; }
    public EditAccomodationRequest[] Accomodations { get; set; }
}

public class EditAccomodationRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
    public int Count { get; set; }
    public List<byte[]> Images { get; set; }
    public ServiceResponse[] Attributes { get; set; }
}