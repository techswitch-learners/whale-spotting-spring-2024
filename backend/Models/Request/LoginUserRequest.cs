namespace WhaleSpotting.Models.Request;

public class LoginUserRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
