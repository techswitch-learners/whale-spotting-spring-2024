namespace WhaleSpotting.Models.Response;

public class ErrorResponse
{
    public Dictionary<string, List<string>> Errors { get; set; } = [];
}
