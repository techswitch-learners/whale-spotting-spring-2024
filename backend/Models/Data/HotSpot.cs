namespace WhaleSpotting.Models.Data;

public class HotSpot
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Latitude { get; set; }

    public required decimal Longitude { get; set; }

    public required string Country { get; set; }

    public List<ViewingSuggestion> ViewingSuggestions { get; set; } = null!;
}
