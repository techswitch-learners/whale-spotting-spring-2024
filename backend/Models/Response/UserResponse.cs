namespace WhaleSpotting.Models.Response;

public class UserResponse
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public string? ProfileImageUrl { get; set; }
}
