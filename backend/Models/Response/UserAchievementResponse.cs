using WhaleSpotting.Models.Data;

namespace WhaleSpotting.Models.Response;

public class UserAchievementResponse
{
    public required int Id { get; set; }
    public required string UserName { get; set; }

    public int Experience { get; set; }

    public required Achievement UserAchievement { get; set; }
}
