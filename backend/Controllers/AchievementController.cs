using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Helpers;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/achievements")]
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
