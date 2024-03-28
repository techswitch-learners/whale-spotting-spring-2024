using System.ComponentModel.DataAnnotations;

namespace WhaleSpotting.Attributes;

public class SightingExistsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var id = (int?)value;

        if (id == null)
        {
            return ValidationResult.Success;
        }

        var context = validationContext.GetService<WhaleSpottingContext>();

        if (context == null)
        {
            throw new ArgumentNullException(nameof(validationContext));
        }

        return context.Sightings.Any(sighting => sighting.Id == id)
            ? ValidationResult.Success
            : new ValidationResult("The specified sighting does not exist.");
    }
}
