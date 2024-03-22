using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/species")]
public class SpeciesController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("")]
    public IActionResult Search()
    {
        var species = _context.Species.ToList();

        var speciesListResponse = new SpeciesListResponse
        {
            SpeciesList = species
                .Select(species => new SpeciesResponse
                {
                    Id = species.Id,
                    Name = species.Name,
                    Description = species.Description,
                    ExampleImageUrl = species.ExampleImageUrl
                })
                .ToList()
        };
        return Ok(speciesListResponse);
    }
}
