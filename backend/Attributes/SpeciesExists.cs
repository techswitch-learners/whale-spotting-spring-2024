using System.ComponentModel.DataAnnotations;

namespace WhaleSpotting.Attributes;

public class SpeciesExistsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var id = (int?)value;
        var context = validationContext.GetService<WhaleSpottingContext>();

        if (context == null)
        {
            throw new ArgumentNullException();
        }

        return context.Species.Any(species => species.Id == id)
            ? ValidationResult.Success
            : new ValidationResult("The specified species does not exist.");
    }
}
