using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scraper.wpf.Validation
{
    public class UrlValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
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
                return new ValidationResult(false, "The Url field must be a valid URI.");
            }

            return ValidationResult.ValidResult;
        }

    }
}
