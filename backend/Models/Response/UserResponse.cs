using WhaleSpotting.Models.Data;

namespace WhaleSpotting.Models.Response;

public class UserResponse
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public required string ProfileImageUrl { get; set; }
    public int Experience { get; set; }
    public required Achievement Achievement { get; set; }
}
