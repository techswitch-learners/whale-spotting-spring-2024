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

    [HttpGet("{sightingId}")]
    public IActionResult GetBySightingId([FromRoute] int sightingId)
    {
        var userId = AuthHelper.GetUserIdIfLoggedIn(User);
        if (_context.Reactions.Any(reaction => reaction.SightingId == sightingId))
        {
            var reactionsDict = _context
                .Reactions.Where(reaction => reaction.SightingId == sightingId)
                .GroupBy(reaction => reaction.Type)
                .ToDictionary(reactionGroup => reactionGroup.Key.ToString(), reactionGroup => reactionGroup.Count());

            var currentUserReaction = _context
                .Reactions.SingleOrDefault(reaction => reaction.SightingId == sightingId && reaction.UserId == userId)
                ?.Type.ToString();

            return Ok(
                new ReactionResponse
                {
                    SightingId = sightingId,
                    Reactions = reactionsDict,
                    CurrentUserReaction = currentUserReaction
                }
            );
        }
        else
        {
            return BadRequest("No reactions exist for this sighting.");
        }
    }

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

            var reactionsDict = _context
                .Reactions.Where(reaction => reaction.SightingId == newReaction.SightingId)
                .GroupBy(reaction => reaction.Type)
                .ToDictionary(reactionGroup => reactionGroup.Key.ToString(), reactionGroup => reactionGroup.Count());

            return Ok(
                new ReactionResponse
                {
                    SightingId = newReaction.SightingId,
                    Reactions = reactionsDict,
                    CurrentUserReaction = addReactionRequest.Type
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
            // _context.Reactions.Update(matchingReaction);
            _context.SaveChanges();

            var reactionsDict = _context
                .Reactions.Where(reaction => reaction.SightingId == matchingReaction.SightingId)
                .GroupBy(reaction => reaction.Type)
                .ToDictionary(reactionGroup => reactionGroup.Key.ToString(), reactionGroup => reactionGroup.Count());

            return Ok(
                new ReactionResponse
                {
                    SightingId = matchingReaction.SightingId,
                    Reactions = reactionsDict,
                    CurrentUserReaction = updateReactionRequest.Type
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

            var reactionsDict = _context
                .Reactions.Where(reaction => reaction.SightingId == matchingReaction.SightingId)
                .GroupBy(reaction => reaction.Type)
                .ToDictionary(reactionGroup => reactionGroup.Key.ToString(), reactionGroup => reactionGroup.Count());

            return Ok(
                new ReactionResponse
                {
                    SightingId = matchingReaction.SightingId,
                    Reactions = reactionsDict,
                    CurrentUserReaction = null
                }
            );
        }
        return BadRequest("Cannot find the matching reaction to delete.");
    }
}
