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
    [MinLength(3, ErrorMessage = "Keywords lenght is out of (3-100)")]
    [MaxLength(100, ErrorMessage = "Keywords lenght is out of (3-100)")]
    public string Keywords { get; set; }
}