namespace TravelCompanyAPI.Helpers;

public class DietTypesConsts
{
    public const string UltraAllInclusive = "UAI";
    public const string AllInclusive = "AI";
    public const string FullBoardPlus = "FB+";
    public const string FullBoard = "FB";
    public const string HalfBoardPlus = "HB+";
    public const string HalfBoard = "HB";
    public const string BedBreakfast = "BB";
    public const string RoomOnly = "RO";

    public static IEnumerable<string> GetAllDiets() =>
        new[]
        {
            UltraAllInclusive,
            AllInclusive,
            FullBoardPlus,
            FullBoard,
            HalfBoardPlus,
            HalfBoard,
            BedBreakfast,
            RoomOnly
        };
}