using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/Hotspots")]
public class HotspotController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var Hotspot = _context
            .Hotspots.Include(Hotspot => Hotspot.ViewingSuggestions)
            .ThenInclude(suggestion => suggestion.Species)
            .SingleOrDefault(Hotspot => Hotspot.Id == id);

        if (Hotspot == null)
        {
            return NotFound();
        }

        return Ok(Hotspot);
    }

    [HttpGet("")]
    public IActionResult Search([FromQuery] SearchHotspotRequest searchRequest)
    {
        var query = _context
            .ViewingSuggestions.Include(suggestion => suggestion.Hotspot)
            .ThenInclude(Hotspot => Hotspot.ViewingSuggestions)
            .ThenInclude(suggestion => suggestion.Species)
            .Include(suggestion => suggestion.Species)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchRequest.Country))
        {
            query = query.Where(suggestion =>
                EF.Functions.ILike(suggestion.Hotspot.Country, $"%{searchRequest.Country}%")
            );
        }
        if (!string.IsNullOrEmpty(searchRequest.HotspotName))
        {
            query = query.Where(suggestion =>
                EF.Functions.ILike(suggestion.Hotspot.Name, $"%{searchRequest.HotspotName}%")
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

        if (query.Count() == 0)
        {
            return NotFound();
        }

        return Ok(query.ToList().GroupBy(suggestion => suggestion.Hotspot).Select(group => group.Key).ToList());
    }
}
