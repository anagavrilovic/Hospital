using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidationRulesManager
{
    class StringToDouble : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;

                if(string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(false, "");
                }

                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Morate uneti brojnu vrednost.");
            }
            catch
            {
                return new ValidationResult(false, "Greška!");
            }
        }
    }
}
