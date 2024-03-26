using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("/reactions")]
public class ReactionsController(WhaleSpottingContext context) : Controller
{
    private readonly WhaleSpottingContext _context = context;
}
