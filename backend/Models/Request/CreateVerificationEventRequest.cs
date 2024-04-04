using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data.Request;

public class CreateVerificationEventRequest
{
    public string? Comment { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }
}
