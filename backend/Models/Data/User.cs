using Microsoft.AspNetCore.Identity;

namespace WhaleSpotting.Models.Data;

public class User : IdentityUser<int>
{
    public required string ProfileImageUrl { get; set; }

    public int Experience { get; set; } = 0;

    public decimal? FavoriteLocationLatitude { get; set; }

    public decimal? FavoriteLocationLongitude { get; set; }

    public List<Sighting> Sightings { get; set; } = [];
}
