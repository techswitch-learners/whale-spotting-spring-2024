using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var achievements = _context.Achievements.ToList();

        return Ok(
            new UserResponse
            {
                Id = matchingUser.Id,
                UserName = matchingUser.UserName!,
                ProfileImageUrl = matchingUser.ProfileImageUrl,
                Experience = matchingUser.Experience,
                Achievement = AchievementHelper.GetAchievementForExperience(achievements, matchingUser.Experience),
            }
        );
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetByUserName([FromRoute] string userName)
    {
        var matchingUser = await _userManager.FindByNameAsync(userName);
        if (matchingUser == null)
        {
            return NotFound();
        }

        var achievements = _context.Achievements.ToList();

        return Ok(
            new UserResponse
            {
                Id = matchingUser.Id,
                UserName = matchingUser.UserName!,
                ProfileImageUrl = matchingUser.ProfileImageUrl,
                Experience = matchingUser.Experience,
                Achievement = AchievementHelper.GetAchievementForExperience(achievements, matchingUser.Experience),
            }
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var achievements = _context.Achievements.ToList();
        var allUsers = await _userManager
            .Users.Select(user => new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                ProfileImageUrl = user.ProfileImageUrl,
                Experience = user.Experience,
                Achievement = AchievementHelper.GetAchievementForExperience(achievements, user.Experience),
            })
            .ToListAsync();

        return Ok(allUsers);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userName)
    {
        var userToRemove = await _userManager.FindByNameAsync(userName);
        if (userToRemove == null)
        {
            return NotFound();
        }
        else
        {
            await _userManager.DeleteAsync(userToRemove);
            return NoContent();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userName}/profile-image")]
    public async Task<IActionResult> ResetProfileImage([FromRoute] string userName)
    {
        var matchingUser = await _userManager.FindByNameAsync(userName);
        if (matchingUser == null)
        {
            return NotFound();
        }
        else
        {
            matchingUser.ProfileImageUrl = string.Empty;
            await _userManager.UpdateAsync(matchingUser);
            return NoContent();
        }
    }
}
