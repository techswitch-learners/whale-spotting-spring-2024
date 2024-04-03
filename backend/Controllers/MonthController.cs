using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/months")]
public class MonthController : Controller
{
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(Enum.GetNames<Month>());
    }
}
