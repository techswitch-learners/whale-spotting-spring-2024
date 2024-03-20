using System.ComponentModel.DataAnnotations;

namespace WhaleSpotting.Attributes;

public class DateTimeNotInFuture : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime d = Convert.ToDateTime(value);
        if (d > DateTime.UtcNow.Date)
            return false;

        return true;
    }
}
