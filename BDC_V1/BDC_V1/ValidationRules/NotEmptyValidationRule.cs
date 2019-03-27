using System.Globalization;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDC_V1.ValidationRules
{
	public class NotEmptyValidationRule : ValidationRulesBase
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, ErrorMessage)
				: ValidationResult.ValidResult;
		}

        public NotEmptyValidationRule()
        {
            ErrorMessage = "field cannot be empty";
        }
    }
}