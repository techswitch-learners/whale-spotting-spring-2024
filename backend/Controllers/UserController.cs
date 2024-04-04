using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Helpers;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/users")]
public class UserController(UserManager<User> userManager, WhaleSpottingContext context) : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly WhaleSpottingContext _context = context;

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var matchingUser = (await _userManager.FindByNameAsync(AuthHelper.GetUserName(User)))!;

        var achievement = _context
            .Achievements.Where(achievement => achievement.MinExperience <= matchingUser.Experience)
            .OrderByDescending(achievement => achievement.MinExperience)
            .First();

        var userView = new UserAchievementResponse
        {
            Id = matchingUser.Id,
            UserName = matchingUser.UserName!,
            Experience = matchingUser.Experience,
            UserAchievement = achievement,
        };
        return Ok(userView);
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetByUserName([FromRoute] string userName)
    {
        var matchingUser = await _userManager.FindByNameAsync(userName);
        if (matchingUser == null)
        {
            return NotFound();
        }

        foreach (var item in _context.Achievements)
        {
            Console.WriteLine(
                $"User {userName}'s Experience: {matchingUser.Experience}, current achievement: {item.Name}"
            );
        }

        var achievement = _context
            .Achievements.Where(achievement => achievement.MinExperience <= matchingUser.Experience)
            .OrderByDescending(achievement => achievement.MinExperience)
            .First();

        var userView = new UserAchievementResponse
        {
            Id = matchingUser.Id,
            UserName = matchingUser.UserName!,
            Experience = matchingUser.Experience,
            UserAchievement = achievement,
        };
        return Ok(userView);
    }
}
