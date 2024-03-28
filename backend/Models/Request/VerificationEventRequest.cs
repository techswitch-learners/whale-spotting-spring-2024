using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data.Request;

public class VerificationEventRequest
{
    public string? Comment { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }
}
