using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WhaleSpotting.Enums;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
    : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
    {
        var matchingUser = await _userManager.FindByNameAsync(loginUserRequest.UserName);
        if (matchingUser != null && await _userManager.CheckPasswordAsync(matchingUser, loginUserRequest.Password))
        {
            var matchingUserRoles = await _userManager.GetRolesAsync(matchingUser);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, matchingUser.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in matchingUserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["Jwt:Secret"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            );
            return Ok(
                new TokenResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                }
            );
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
    {
        var newUser = new User { UserName = registerUserRequest.UserName, };
        var result = await _userManager.CreateAsync(newUser, registerUserRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(
                new ErrorResponse { Errors = result.Errors.Select(error => error.Description).ToList(), }
            );
        }
        await _userManager.AddToRoleAsync(newUser, RoleType.User.ToString());
        return CreatedAtAction(
            nameof(UserController.GetByUserName),
            nameof(UserController)[..^"Controller".Length],
            new { newUser.UserName },
            new UserResponse { Id = newUser.Id, UserName = newUser.UserName, }
        );
    }
}
