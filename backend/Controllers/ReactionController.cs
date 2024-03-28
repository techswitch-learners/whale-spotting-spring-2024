using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Enums;
using WhaleSpotting.Helpers;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

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
            ) && Enum.TryParse<ReactionType>(addReactionRequest.Type, true, out var reactionType)
        )
        {
            var newReaction = _context
                .Reactions.Add(
                    new Reaction
                    {
                        Type = reactionType,
                        UserId = userId,
                        SightingId = addReactionRequest.SightingId,
                        Timestamp = DateTime.UtcNow
                    }
                )
                .Entity;
            _context.SaveChanges();
            return Ok(
                new ReactionResponse
                {
                    Type = newReaction.Type.ToString(),
                    UserId = newReaction.UserId,
                    SightingId = newReaction.SightingId,
                    Timestamp = newReaction.Timestamp
                }
            );
        }

        return BadRequest("You can not give reaction to the same sighting more than once");
    }

    [Authorize]
    [HttpPatch("")]
    public IActionResult UpdateReaction([FromBody] UpdateReactionRequest updateReactionRequest)
    {
        var userId = AuthHelper.GetUserId(User);
        var matchingReaction = _context.Reactions.SingleOrDefault(reaction =>
            reaction.SightingId == updateReactionRequest.SightingId && reaction.UserId == userId
        );

        if (
            matchingReaction != null
            && Enum.TryParse<ReactionType>(updateReactionRequest.Type, true, out var reactionType)
        )
        {
            matchingReaction.Type = reactionType;
            matchingReaction.Timestamp = DateTime.UtcNow;
            _context.SaveChanges();
            return Ok(
                new ReactionResponse
                {
                    Type = matchingReaction.Type.ToString(),
                    UserId = matchingReaction.UserId,
                    SightingId = matchingReaction.SightingId,
                    Timestamp = matchingReaction.Timestamp
                }
            );
        }
        return BadRequest("Cannot find the matching reaction to update.");
    }

    [Authorize]
    [HttpDelete("")]
    public IActionResult DeleteReaction([FromBody] DeleteReactionRequest deleteReactionRequest)
    {
        var userId = AuthHelper.GetUserId(User);
        var matchingReaction = _context.Reactions.SingleOrDefault(reaction =>
            reaction.SightingId == deleteReactionRequest.SightingId && reaction.UserId == userId
        );

        if (matchingReaction != null)
        {
            _context.Reactions.Remove(matchingReaction);
            _context.SaveChanges();
            return Ok("Deleted Reaction Successfully.");
        }
        return BadRequest("Cannot find the matching reaction to delete.");
    }
}
