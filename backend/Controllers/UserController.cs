using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
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
        var filteredUsers = new List<AdminUserResponse>();

        foreach (var user in allUsers)
        {
            var existingUser = new AdminUserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                ProfileImageUrl = user.ProfileImageUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AccessFailedCount = user.AccessFailedCount,
            };
            filteredUsers.Add(existingUser);
        }
        return Ok(filteredUsers);
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
    public async Task<IActionResult> EditUserInfo([FromRoute] int userId, [FromBody] EditUserRequest editUserRequest)
    {
        var matchingUser = await _userManager.FindByIdAsync(userId.ToString());
        if (matchingUser == null)
        {
            return NotFound();
        }
        else
        {
            if (editUserRequest.UserName != null)
            {
                matchingUser.UserName = editUserRequest.UserName;
            }
            if (editUserRequest.ProfileImageUrl != null)
            {
                matchingUser.ProfileImageUrl = editUserRequest.ProfileImageUrl;
            }
            if (editUserRequest.Email != null)
            {
                matchingUser.Email = editUserRequest.Email;
            }
            if (editUserRequest.PhoneNumber != null)
            {
                matchingUser.PhoneNumber = editUserRequest.PhoneNumber;
            }
            ;
            await _userManager.UpdateAsync(matchingUser);
            return Ok();
        }
    }
}
