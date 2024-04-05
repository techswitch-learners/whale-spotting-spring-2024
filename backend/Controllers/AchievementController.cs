using Microsoft.AspNetCore.Mvc;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/achievements")]
public class AchievementController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var achievements = _context.Achievements.ToList();

        return Ok(achievements);
    }
}
