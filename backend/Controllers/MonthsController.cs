using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/months")]
public class MonthsController(WhaleSpottingContext context) : Controller
{
    [HttpGet("")]
    public IActionResult Search()
    {
        List<string> monthList = [];
        for (var i = 0; i < 12; i++)
        {
            monthList.Add(((Month)i).ToString());
        }

        return Ok(monthList);
    }
}
