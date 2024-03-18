using System.ComponentModel.DataAnnotations;

namespace WhaleSpotting.Models.Request;

public class RegisterUserRequest
{
    [RegularExpression(@"[a-z0-9]+", ErrorMessage = "Usernames can only contain lowercase letters and digits.")]
    [Length(minimumLength: 1, maximumLength: 32, ErrorMessage = "Usernames must be between 1 and 32 characters long.")]
    public required string UserName { get; set; }

    public required string Password { get; set; }
}
