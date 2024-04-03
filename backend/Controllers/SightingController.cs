using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Data.Request;
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
                    BodyOfWater = addSightingRequest.BodyOfWater,
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
            .Include(sighting => sighting.VerificationEvent)
            .Include(sighting => sighting.Reactions)
            .Where(sighting => sighting.VerificationEvent != null)
            .SingleOrDefault(sighting => sighting.Id == id);
        if (matchingSighting == null || (int)matchingSighting.VerificationEvent!.ApprovalStatus == 0)
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
            .Include(sighting => sighting.VerificationEvent)
            .Include(sighting => sighting.Reactions)
            .Where(sighting =>
                sighting.VerificationEvent != null && (int)sighting.VerificationEvent.ApprovalStatus == 1
            )
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
                    Reactions = sighting.Reactions
                })
                .ToList()
        };
        return Ok(sightingsResponse);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("pending")]
    public IActionResult ViewPendingSightings()
    {
        var pendingSightings = _context
            .Sightings.Include(sighting => sighting.User)
            .Include(sighting => sighting.Species)
            .Include(sighting => sighting.VerificationEvent)
            .Include(sighting => sighting.Reactions)
            .Where(sighting => sighting.VerificationEvent == null)
            .ToList();

        var sightingsResponse = new SightingsResponse
        {
            Sightings = pendingSightings
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
                    Reactions = sighting.Reactions
                })
                .ToList()
        };
        return Ok(sightingsResponse);
    }

    [HttpPost("{sightingId}/verify")]
    public IActionResult VerifySighting(
        [FromRoute] int sightingId,
        [FromBody] VerificationEventRequest verificationEventRequest
    )
    {
        var sighting = _context.Sightings.FirstOrDefault(sighting => sighting.Id == sightingId);
        var userId = AuthHelper.GetUserId(User);

        if (sighting == null)
        {
            return NotFound();
        }
        else
        {
            var newVerificationEvent = new VerificationEvent
            {
                SightingId = sightingId,
                AdminId = userId,
                ApprovalStatus = verificationEventRequest.ApprovalStatus,
                Comment = verificationEventRequest.Comment,
                Timestamp = DateTime.UtcNow,
            };

            var savedVerificationEvent = _context.VerificationEvents.Add(newVerificationEvent).Entity;
            _context.SaveChanges();
            sighting.VerificationEventId = savedVerificationEvent.Id;
            _context.SaveChanges();
            return Ok();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{sightingId}")]
    public IActionResult DeleteSighting([FromRoute] int sightingId)
    {
        var sightingToRemove = _context.Sightings.FirstOrDefault(sighting => sighting.Id == sightingId);
        if (sightingToRemove == null)
        {
            return NotFound();
        }
        else
        {
            _context.Sightings.Remove(sightingToRemove);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
