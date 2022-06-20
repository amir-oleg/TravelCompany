namespace TravelCompanyAPI.Helpers;

public static class TrasnportTypesConsts
{
    public const string AirPlane = "AI";
    public const string Train = "RL";
    public const string Bus = "BS";

    public static IEnumerable<string> GetAll()
    {
        return new[] { AirPlane, Train, Bus };
    }
}