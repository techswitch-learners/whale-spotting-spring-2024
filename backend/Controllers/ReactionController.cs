using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // [Authorize]
    // [HttpPatch("")]
    // public IActionResult Update([FromBody] AddReactionRequest reactionRequest)
    // {
    //     var userId = AuthHelper.GetUserId(User);
    //     var matchingReaction = _context
    //         .Reactions.Where(reaction=> reaction.UserId == userId && reaction.SightingId==reactionRequest.SightingId);

    //     matchingReaction
    //     _context.Reactions.Update()
    //     return Ok(newReaction);
    // }
}
