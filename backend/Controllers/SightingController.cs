using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/sighting")]
public class SightingController : Controller
{
    private readonly WhaleSpottingContext _whaleSpotting;
    private readonly UserManager<User> _userManager;

    public SightingController(WhaleSpottingContext whaleSpottingContext, UserManager<User> userManager)
    {
        _whaleSpotting = whaleSpottingContext;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SightingRequest sightingRequest)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(sightingRequest.Token) as JwtSecurityToken;
        var nameClaim = jsonToken?.Claims.FirstOrDefault(claim =>
            claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        );
        var userName = nameClaim?.Value;
        Console.WriteLine($"***********{userName}");

        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidmlja3k5NCIsImp0aSI6Ijk5MGRmZDYyLTYzZjAtNDY2ZS1hOGQyLWNjNDA2NDVhYjNkZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MTA4NjI0MTIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTI4MCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE3MyJ9.QAp-nI3VyrREwfMp6IIG9MtzYUXNCF2kQmIC9fLQaU0
        //Nandini -
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibmFuZGluaXAiLCJqdGkiOiI2YjY0N2JkNi1lMmUxLTQyZDMtOTc0Mi0xNTNhZmNkMjVlZjMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNzEwOTMzMDg1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyODAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxNzMifQ.zZ_e5Mr_-Jx-96uW0iJEMHOHURNFr8_qRC-1wP9qQhY
        //var userId = _whaleSpotting.Users.Single(user => user.UserName==userName).Id;
        var matchingUser = await _userManager.FindByNameAsync(userName);
        var userId = matchingUser.Id;
        Console.WriteLine($"*****{userId}");

        var newSighting = _whaleSpotting
            .Sightings.Add(
                new Sighting
                {
                    Latitude = sightingRequest.Latitude,
                    Longitude = sightingRequest.Longitude,
                    UserId = userId,
                    SpeciesId = sightingRequest.SpeciesId,
                    Description = sightingRequest.Description,
                    ImageUrl = sightingRequest.ImageUrl,
                    BodyOfWaterId = sightingRequest.BodyOfWaterId,
                    SightingTimestamp = sightingRequest.SightingTimestamp,
                    CreationTimestamp = DateTime.UtcNow,
                }
            )
            .Entity;
        _whaleSpotting.SaveChanges();
        return Ok(newSighting);
    }
}
