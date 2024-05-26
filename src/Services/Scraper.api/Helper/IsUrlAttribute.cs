using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Scraper.api.Helper;

public class IsUrlAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var urlRegex = new Regex(
            @"^(https?)" +
            @":\/\/" +
            @"(www\.)?" +
            @"([a-zA-Z0-9\-]+\.)+" +
            "[a-zA-Z]{2,}" +
            @"(\/[a-zA-Z0-9?=&]+)*\/?$",
            RegexOptions.IgnoreCase);
        
        string? url = value as string;

        if (url == null || !urlRegex.IsMatch(url))
        {
            return new ValidationResult("The Url field must be a valid URI.");
        }

        return ValidationResult.Success;
    }
}