using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Helpers;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/sightings")]
public class SightingController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [Authorize]
    [HttpPost("")]
    public IActionResult Add([FromBody] AddSightingRequest addSightingRequest)
    {
        var userId = AuthHelper.GetUserId(User);
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
        var userId = AuthHelper.GetUserIdIfLoggedIn(User);

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
            BodyOfWater = matchingSighting.BodyOfWater,
            VerificationEvent = matchingSighting.VerificationEvent,
            SightingTimestamp = matchingSighting.SightingTimestamp,
            CreationTimestamp = matchingSighting.CreationTimestamp,
            Reactions = matchingSighting
                .Reactions.GroupBy(reaction => reaction.Type)
                .ToDictionary(reactionGroup => reactionGroup.Key.ToString(), reactionGroup => reactionGroup.Count()),
            CurrentUserReaction =
                userId != null
                    ? matchingSighting.Reactions.SingleOrDefault(reaction => reaction.UserId == userId)?.Type.ToString()
                    : null
        };

        return Ok(sighting);
    }

    [HttpGet("")]
    public IActionResult Search()
    {
        var userId = AuthHelper.GetUserIdIfLoggedIn(User);

        Console.WriteLine($"=================={userId}");

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
                    BodyOfWater = sighting.BodyOfWater,
                    VerificationEvent = sighting.VerificationEvent,
                    SightingTimestamp = sighting.SightingTimestamp,
                    CreationTimestamp = sighting.CreationTimestamp,
                    Reactions = sighting
                        .Reactions.GroupBy(reaction => reaction.Type)
                        .ToDictionary(
                            reactionGroup => reactionGroup.Key.ToString(),
                            reactionGroup => reactionGroup.Count()
                        ),
                    CurrentUserReaction =
                        userId != null
                            ? sighting.Reactions.SingleOrDefault(reaction => reaction.UserId == userId)?.Type.ToString()
                            : null
                })
                .ToList()
        };
        return Ok(sightingsResponse);
    }
}
