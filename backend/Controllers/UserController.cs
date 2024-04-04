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
        var allUsers = await _userManager
            .Users.Select(user => new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                ProfileImageUrl = user.ProfileImageUrl,
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
    [HttpPatch("{userName}/profile-picture")]
    public async Task<IActionResult> EditProfilePicture([FromRoute] string userName, [FromBody] string imageURL)
    {
        var matchingUser = await _userManager.FindByNameAsync(userName);
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
