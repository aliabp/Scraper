using System.Net;
using Microsoft.AspNetCore.Mvc;
using Scraper.api.Helper;
using Scraper.api.Models;
using Scraper.api.Services;

namespace Scraper.api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ScraperController : ControllerBase
{
    private readonly IScraperService _scraperService;
    
    public ScraperController(IScraperService scraperService)
    {
        // inject scraper service using IoC container
        _scraperService = scraperService;
    }

    // return not found
    //return error happend
    [HttpPost(Name = "Scraper")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(((int)HttpStatusCode.BadRequest))]
    public async Task<IActionResult> Scraper([FromBody] ScraperModel model)
    {
        // check modelstate validation
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            // generate search address including keywords
            string googleUrl = $"https://www.google.com.au/search?num=100&q={Uri.EscapeDataString(model.Keywords)}";
            
            // scrape the URL and find the target URL in search result using scraper service class
            var positions = await _scraperService.ScrapeAndParseAsync(googleUrl, model.Url);
        
            return Ok(string.Join(",", positions));
        }
        catch (Exception e)
        {
            LogHelper.Instance.LogError($"Something went wrong inside Scraper action: {e.Message}");
            return StatusCode(500, "Internal server error");
        }

    }
}