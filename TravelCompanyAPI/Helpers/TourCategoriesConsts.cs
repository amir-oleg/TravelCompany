namespace TravelCompanyAPI.Helpers;

public class TourCategoriesConsts
{
    public const string Beaches = "BC";
    public const string CorpVacation = "CP";
    public const string ChildrenVacation = "CV";
    public const string Excursion = "EX";
    public const string Islands = "IL";
    public const string Ski = "SK";
    public const string Sport = "SP";
    public const string Spa = "ST";
    public const string Vip = "VP";
    public const string WithoutChildren = "WC";
    public const string Wedding = "WE";

    public static IEnumerable<string> GetAllCategories() =>
        new[]
        {
            Beaches,
            CorpVacation,
            ChildrenVacation,
            Excursion,
            Islands,
            Ski,
            Sport,
            Spa,
            Vip,
            WithoutChildren,
            Wedding
        };
}