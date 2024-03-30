using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Request;

public class SearchHotspotRequest
{
    public string? Country { get; set; }
    public string? HotspotName { get; set; }
    public List<string>? Species { get; set; }
    public List<Platform>? Platforms { get; set; }
    public List<Month>? Months { get; set; }
}
