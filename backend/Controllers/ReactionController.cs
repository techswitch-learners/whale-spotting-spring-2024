using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Enums;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/reactions")]
public class ReactionController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [Authorize]
    [HttpPost("")]
    public IActionResult Add([FromBody] AddReactionRequest addReactionRequest)
    {
        var userId = AuthHelper.GetUserId(User);
        if (
            !_context.Reactions.Any(reaction =>
                reaction.SightingId == addReactionRequest.SightingId && reaction.UserId == userId
            )
        )
        {
            var newReaction = _context
                .Reactions.Add(
                    new Reaction
                    {
                        Type = addReactionRequest.Type,
                        UserId = userId,
                        SightingId = addReactionRequest.SightingId,
                        Timestamp = DateTime.UtcNow
                    }
                )
                .Entity;
            _context.SaveChanges();
            return Ok(newReaction);
        }

        return BadRequest("You can not give reaction to the same sighting more than once");
    }

    [Authorize]
    [HttpPatch("")]
    public IActionResult UpdateReaction([FromBody] AddReactionRequest addReactionRequest)
    {
        var userId = AuthHelper.GetUserId(User);
        var matchingSighting = _context.Reactions.SingleOrDefault(reaction =>
            reaction.SightingId == addReactionRequest.SightingId && reaction.UserId == userId
        );
        if (matchingSighting != null)
        {
            ReactionType newReactionType = addReactionRequest.Type;
            if (matchingSighting.Type != newReactionType)
            {
                matchingSighting.Type = newReactionType;
                matchingSighting.Timestamp = DateTime.UtcNow;
                _context.SaveChanges();

                return Ok(matchingSighting);
            }
        }
        else
        {
            return BadRequest("Reaction type is the same as before. No update made.");
        }

        return NotFound("No existing reaction found for the given sighting by this user.");
    }
}
