using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/users")]
public class UserController(UserManager<User> userManager) : Controller
{
    private readonly UserManager<User> _userManager = userManager;

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var matchingUser = (await _userManager.FindByNameAsync(AuthHelper.GetUserName(User)))!;
        return Ok(new UserResponse { Id = matchingUser.Id, UserName = matchingUser.UserName!, });
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetByUserName([FromRoute] string userName)
    {
        var matchingUser = await _userManager.FindByNameAsync(userName);
        if (matchingUser == null)
        {
            return NotFound();
        }
        return Ok(new UserResponse { Id = matchingUser.Id, UserName = matchingUser.UserName!, });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var allUsers = await _userManager.Users.ToListAsync();
        var existingUsers = new List<UserResponse>();

        foreach (var user in allUsers)
        {
            var existingUser = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            existingUsers.Add(existingUser);
        }
        return Ok(existingUsers);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        var userToRemove = await _userManager.FindByIdAsync(userId.ToString());
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
    [HttpPatch("edit/{userId}")]
    public async Task<IActionResult> EditProfilePicture([FromRoute] int userId, [FromBody] string imageURL)
    {
        var matchingUser = await _userManager.FindByIdAsync(userId.ToString());
        if (matchingUser == null)
        {
            return NotFound();
        }
        else
        {
            matchingUser.ProfileImageUrl = imageURL;
            await _userManager.UpdateAsync(matchingUser);
            return NoContent();
        }
    }
}
