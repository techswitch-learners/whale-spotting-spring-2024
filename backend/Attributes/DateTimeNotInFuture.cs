using System.ComponentModel.DataAnnotations;

namespace WhaleSpotting.Attributes;

public class DateTimeNotInFuture : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var dateTime = (DateTime?)value;

        if (dateTime == null)
        {
            return true;
        }

        return dateTime <= DateTime.UtcNow;
    }
}
