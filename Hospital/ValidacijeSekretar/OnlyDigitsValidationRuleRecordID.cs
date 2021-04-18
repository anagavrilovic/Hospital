using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidacijeSekretar
{
    class OnlyDigitsValidationRuleRecordID : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string stringValue = value as string;
                Regex r = new Regex(@"^[0-9]+$");

                if (stringValue.Length == 0)
                {
                    return new ValidationResult(false, "");
                }

                if (r.IsMatch(stringValue))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Ovo polje treba da sadrži samo cifre!");
            }
            catch
            {
                return new ValidationResult(false, "Nepoznata greška");
            }
        }
    }
}
