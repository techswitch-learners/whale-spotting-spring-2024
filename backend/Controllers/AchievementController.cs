using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/achievement")]
public class AchievementController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("")]
    public IActionResult GetAchievements()
    {
        var achievements = _context.Achievements.ToList();

        return Ok(achievements);
    }
}
