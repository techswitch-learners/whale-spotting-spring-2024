namespace WhaleSpotting.Models.Request;

public class EditUserRequest
{
    public string? UserName { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
