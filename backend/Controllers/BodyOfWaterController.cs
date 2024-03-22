using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/bodies-of-water")]
public class BodyOfWaterController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("")]
    public IActionResult Search()
    {
        var bodiesOfWater = _context.BodiesOfWater.ToList();

        var bodiesOfWaterResponse = new BodiesOfWaterResponse
        {
            BodiesOfWater = bodiesOfWater
                .Select(bodyOfWater => new BodyOfWaterResponse { Id = bodyOfWater.Id, Name = bodyOfWater.Name })
                .ToList()
        };
        return Ok(bodiesOfWaterResponse);
    }
}
