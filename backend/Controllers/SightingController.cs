using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using WhaleSpotting.Attributes;
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

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddSightingRequest addSightingRequest)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(addSightingRequest.Token) as JwtSecurityToken;
        var nameClaim = jsonToken?.Claims.FirstOrDefault(claim =>
            claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        );
        var userName = nameClaim?.Value;
        if (userName == null)
        {
            return NotFound();
        }
        var matchingUser = await _userManager.FindByNameAsync(userName);
        var userId = matchingUser.Id;

        var newSighting = _whaleSpotting
            .Sightings.Add(
                new Sighting
                {
                    Latitude = addSightingRequest.Latitude,
                    Longitude = addSightingRequest.Longitude,
                    UserId = userId,
                    SpeciesId = addSightingRequest.SpeciesId,
                    Description = addSightingRequest.Description,
                    ImageUrl = addSightingRequest.ImageUrl,
                    BodyOfWaterId = addSightingRequest.BodyOfWaterId,
                    SightingTimestamp = addSightingRequest.SightingTimestamp,
                    CreationTimestamp = DateTime.UtcNow,
                }
            )
            .Entity;
        _whaleSpotting.SaveChanges();
        return Ok(newSighting);
    }

    [HttpGet("all")]
    public IActionResult ListAll()
    {
        var sightingsResponse = new SightingsResponse();
        var sightingsList = _whaleSpotting
            .Sightings.Include(s => s.User)
            .Include(s => s.Species)
            .Include(s => s.BodyOfWater)
            .Include(s => s.VerificationEvent)
            .Include(s => s.Reactions)
            .ToList();

        foreach (var sighting in sightingsList)
        {
            var sightingResponse = new SightingResponse
            {
                Id = sighting.Id,
                Latitude = sighting.Latitude,
                Longitude = sighting.Longitude,
                UserName = string.IsNullOrEmpty(sighting.User.UserName) ? "" : sighting.User.UserName,
                Species = sighting.Species,
                Description = sighting.Description,
                ImageUrl = sighting.ImageUrl,
                BodyOfWaterName = sighting.BodyOfWater.Name,
                VerificationEvent = sighting.VerificationEvent,
                SightingTimestamp = sighting.SightingTimestamp,
                CreationTimestamp = sighting.CreationTimestamp,
                Reactions = sighting.Reactions
            };
            sightingsResponse.Sightings.Add(sightingResponse);
        }

        return Ok(sightingsResponse);
    }
}
