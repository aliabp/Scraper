using System.ComponentModel.DataAnnotations;
using Scraper.api.Helper;

namespace Scraper.api.Models;

// model for scraper
public class ScraperModel
{
    // url which should find in searcg result
    [Required(ErrorMessage = "URL Required")]
    [IsUrl]
    public string Url { get; set; }
    
    // keywords for search
    [Required(ErrorMessage = "Keywords Required")]
    [Range(3, 100, ErrorMessage = "Keywords out of range (3 - 100)")]
    public string Keywords { get; set; }
}