using Microsoft.AspNetCore.Mvc;

namespace Fortius.CodingChallenge.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public sealed class SearchEngineController(ISearchEngine searchEngine) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult?> Get([FromQuery] SearchOptions options)
    {
        var response = await searchEngine.FetchOrGetResults(options);
        try
        {
            var json = new JsonResult(new
            {
                response.Shirts,
                response.SizeCounts,
                response.ColorCounts
            });
            return json;
        }
        catch (Exception)
        {
            return new JsonResult(new { success = false, message = "An error occured fetching your results" });
        }
    }
}