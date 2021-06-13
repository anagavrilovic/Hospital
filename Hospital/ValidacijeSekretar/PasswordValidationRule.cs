using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidacijeSekretar
{
    class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;

            if (stringValue.Length == 0)
            {
                return new ValidationResult(true, "");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(stringValue, @"^(?=.*\d)(?=.*[a-zA-Z]).$"))
            {
                return new ValidationResult(false, "Lozinka mora sadržati bar jedno veliko slovo, jednu cifru i jedno malo slovo!");
            }

            if (stringValue.Length < 8)
            {
                return new ValidationResult(false, "Lozinka mora imati bar 8 karaktera!");
            }

            return new ValidationResult(true, null);
        }
    }
}
