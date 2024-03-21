using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/sightings")]
public class SightingController(WhaleSpottingContext context, UserManager<User> userManager) : Controller
{
    private readonly WhaleSpottingContext _context = context;
    private readonly UserManager<User> _userManager = userManager;

    [HttpPost("")]
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
        var userId = matchingUser!.Id;

        var newSighting = _context
            .Sightings.Add(
                new Sighting
                {
                    Latitude = addSightingRequest.Latitude!.Value,
                    Longitude = addSightingRequest.Longitude!.Value,
                    UserId = userId,
                    SpeciesId = addSightingRequest.SpeciesId!.Value,
                    Description = addSightingRequest.Description,
                    ImageUrl = addSightingRequest.ImageUrl,
                    BodyOfWaterId = addSightingRequest.BodyOfWaterId!.Value,
                    SightingTimestamp = addSightingRequest.SightingTimestamp!.Value,
                    CreationTimestamp = DateTime.UtcNow,
                }
            )
            .Entity;
        _context.SaveChanges();
        return Ok(newSighting);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var matchingSighting = _context
            .Sightings.Include(sighting => sighting.User)
            .Include(sighting => sighting.Species)
            .Include(sighting => sighting.BodyOfWater)
            .Include(sighting => sighting.VerificationEvent)
            .Include(sighting => sighting.Reactions)
            .SingleOrDefault(sighting => sighting.Id == id);
        if (matchingSighting == null)
        {
            return NotFound();
        }
        var sighting = new SightingResponse
        {
            Id = matchingSighting.Id,
            Latitude = matchingSighting.Latitude,
            Longitude = matchingSighting.Longitude,
            UserName = matchingSighting.User.UserName!,
            Species = matchingSighting.Species,
            Description = matchingSighting.Description,
            ImageUrl = matchingSighting.ImageUrl,
            BodyOfWaterName = matchingSighting.BodyOfWater.Name,
            VerificationEvent = matchingSighting.VerificationEvent,
            SightingTimestamp = matchingSighting.SightingTimestamp,
            CreationTimestamp = matchingSighting.CreationTimestamp,
            Reactions = matchingSighting.Reactions
        };

        return Ok(sighting);
    }

    [HttpGet("")]
    public IActionResult Search()
    {
        var sightings = _context
            .Sightings.Include(sighting => sighting.User)
            .Include(sighting => sighting.Species)
            .Include(sighting => sighting.BodyOfWater)
            .Include(sighting => sighting.VerificationEvent)
            .Include(sighting => sighting.Reactions)
            .ToList();

        var sightingsResponse = new SightingsResponse
        {
            Sightings = sightings
                .Select(sighting => new SightingResponse
                {
                    Id = sighting.Id,
                    Latitude = sighting.Latitude,
                    Longitude = sighting.Longitude,
                    UserName = sighting.User.UserName!,
                    Species = sighting.Species,
                    Description = sighting.Description,
                    ImageUrl = sighting.ImageUrl,
                    BodyOfWaterName = sighting.BodyOfWater.Name,
                    VerificationEvent = sighting.VerificationEvent,
                    SightingTimestamp = sighting.SightingTimestamp,
                    CreationTimestamp = sighting.CreationTimestamp,
                    Reactions = sighting.Reactions
                })
                .ToList()
        };
        return Ok(sightingsResponse);
    }
}
