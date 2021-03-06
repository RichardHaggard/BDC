﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace BDC_V1.ValidationRules
{
    public class IsCheckedValidationRule : ValidationRulesBase
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is bool b && b)
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, ErrorMessage);
        }

        public IsCheckedValidationRule()
        {
            ErrorMessage = "box must be checked";
        }
    }
}