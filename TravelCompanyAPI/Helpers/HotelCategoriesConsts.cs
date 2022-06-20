namespace TravelCompanyAPI.Helpers;

public class HotelCategoriesConsts
{
    public const string FiveStars = "5S";
    public const string FourStars = "4S";
    public const string ThreeStars = "3S";
    public const string TwoStars = "2S";
    public const string Apartments = "Apts";
    public const string HolidayVillageOne = "H1";
    public const string HolidayVillageTwo = "H2";
    public const string Villas = "VI";

    public static IEnumerable<string> GetAllCategories() =>
        new[]
        {
            FiveStars,
            FourStars,
            ThreeStars,
            TwoStars,
            Apartments,
            HolidayVillageOne,
            HolidayVillageTwo,
            Villas
        };
}