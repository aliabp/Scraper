using System.ComponentModel.DataAnnotations;

namespace Scraper.api.Models;

public class ApplicationOptions
{
    public const string Key = "Application";

    [Required(ErrorMessage = "UrlHolderTagName Required")]
    public string UrlHolderTagName { get; set; }
    
}