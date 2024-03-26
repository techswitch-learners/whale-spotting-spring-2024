namespace WhaleSpotting.Models.Transfer;

public class HotSpotRow
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string Species { get; set; }
    public required string Region { get; set; }
    public required string TownOrHarbour { get; set; }
    public required string Platform { get; set; }
    public required string PlatformBoxes { get; set; }
    public required string TimeOfYear { get; set; }
    public required string Months { get; set; }
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
}
