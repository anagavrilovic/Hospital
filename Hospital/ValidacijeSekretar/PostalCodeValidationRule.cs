using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidacijeSekretar
{
    class PostalCodeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;

            if (stringValue.Length == 0)
            {
                return new ValidationResult(true, "");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(stringValue, @"^[0-9]+$"))
            {
                return new ValidationResult(false, "Ovo polje treba da sadrži samo cifre!");
            }
            else if (stringValue.Length != 5)
            {
                return new ValidationResult(false, "Ovo polje sadrži 5 cifara!");
            }

            return new ValidationResult(true, null);
        }
    }
}
