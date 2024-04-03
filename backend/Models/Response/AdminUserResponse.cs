namespace WhaleSpotting.Models.Response;

public class AdminUserResponse
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public required string ProfileImageUrl { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? AccessFailedCount { get; set; }
}
