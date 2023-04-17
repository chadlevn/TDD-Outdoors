namespace Domain.Constants;

public sealed class ActivityTypeConstants
{
    public const string Biking = "Biking";
    public const string Camping = "Camping";
    public const string Fishing = "Fishing";
    public const string Hiking = "Hiking";

    public static List<string> AllActivityTypes = new()
    {
        Biking,
        Camping,
        Fishing,
        Hiking
    };
}