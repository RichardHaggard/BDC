using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

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