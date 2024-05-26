using System.Globalization;
using System.Windows.Controls;

namespace Scraper.wpf.Validation;

public class KeywordsValidation : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {

        string? keywords = value as string;

        // check keywords input lenght is between 3 to 100
        if (keywords == null || keywords.Length < 3 || keywords.Length > 100)
        {
            return new ValidationResult(false, "The keywords field is out of 3-100.");
        }

        return ValidationResult.ValidResult;
    }

}