using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/hotspots")]
public class HotSpotsController : Controller
{
    private readonly WhaleSpottingContext _context;

    public HotSpotsController(WhaleSpottingContext context)
    {
        _context = context;
    }

    /* create an endpoint to list all the hotspots */
    [HttpGet("")]
    public IActionResult GetAllHotSpots()
    {
        return Ok(_context.HotSpots.ToList());
    }

    /* create an endpoint to list all the viewing suggestions */
    [HttpGet("viewing-suggestion")]
    public IActionResult GetAllSuggestions()
    {
        return Ok(_context.ViewingSuggestions.ToList());
    }

    /* create an endpoint to allow user to search in our database */
    [HttpGet("search")]
    public IActionResult Search([FromQuery] SearchHotSpotRequest searchRequest)
    {
        var query = _context
            .ViewingSuggestions.Include(suggestion => suggestion.HotSpot)
            .Include(suggestion => suggestion.Species)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchRequest.Country))
        {
            query = query.Where(suggestion => suggestion.HotSpot.Country.Contains(searchRequest.Country));
        }
        if (!string.IsNullOrEmpty(searchRequest.HotSpotName))
        {
            query = query.Where(suggestion => suggestion.HotSpot.Name.Contains(searchRequest.HotSpotName));
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

        return Ok(query.ToList());
    }
}
