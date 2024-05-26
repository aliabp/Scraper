using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Scraper.wpf.Validation;

public class UrlValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        // standard URL regular expression
        var urlRegex = new Regex(
                            @"^(https?)" +
                            @":\/\/" +
                            @"(www\.)?" +
                            @"([a-zA-Z0-9\-]+\.)+" +
                            "[a-zA-Z]{2,}" +
                            @"(\/[a-zA-Z0-9?=&]+)*\/?$",
                            RegexOptions.IgnoreCase);

        string? url = value as string;

        // check URL is valid and well format
        if (url == null || !urlRegex.IsMatch(url))
        {
            return new ValidationResult(false, "The Url field must be a valid URI.");
        }

        return ValidationResult.ValidResult;
    }

}