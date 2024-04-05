using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data.Request;

public class CreateVerificationEventRequest
{
    [Required(AllowEmptyStrings = true, ErrorMessage = "Comment is required.")]
    public required string Comment { get; set; }

    [Required(ErrorMessage = "Approval status is required.")]
    public required ApprovalStatus? ApprovalStatus { get; set; }
}
