using System.Globalization;
using System.Windows.Controls;

namespace DbTableToDotnetEntity.Domain
{
    internal class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "此项为必输项")
                : ValidationResult.ValidResult;
        }
    }
}
