using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/hotspots")]
public class HotSpotController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("")]
    public IActionResult Search([FromQuery] SearchHotSpotRequest searchRequest)
    {
        var query = _context
            .ViewingSuggestions.Include(suggestion => suggestion.HotSpot)
            .ThenInclude(hotSpot => hotSpot.ViewingSuggestions)
            .ThenInclude(suggestion => suggestion.Species)
            .Include(suggestion => suggestion.Species)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchRequest.Country))
        {
            query = query.Where(suggestion =>
                EF.Functions.ILike(suggestion.HotSpot.Country, $"%{searchRequest.Country}%")
            );
        }
        if (!string.IsNullOrEmpty(searchRequest.HotSpotName))
        {
            query = query.Where(suggestion =>
                EF.Functions.ILike(suggestion.HotSpot.Name, $"%{searchRequest.HotSpotName}%")
            );
        }
        if (searchRequest.Species != null && searchRequest.Species.Count > 0)
        {
            query = query.Where(suggestion => searchRequest.Species.Contains(suggestion.Species.Name));
        }
        if (searchRequest.Platforms != null && searchRequest.Platforms.Count > 0)
        {
            query = query.Where(suggestion =>
                searchRequest.Platforms.Any(platform => suggestion.PlatformBoxes.Contains(platform))
            );
        }
        if (searchRequest.Months != null && searchRequest.Months.Count > 0)
        {
            query = query.Where(suggestion => searchRequest.Months.Any(month => suggestion.Months.Contains(month)));
        }

        return Ok(query.ToList().GroupBy(suggestion => suggestion.HotSpot).Select(group => group.Key).ToList());
    }
}
