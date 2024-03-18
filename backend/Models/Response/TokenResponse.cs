namespace WhaleSpotting.Models.Response;

public class TokenResponse
{
    public required string Token { get; set; }
    public required DateTime Expiration { get; set; }
}
