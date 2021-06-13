using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidationRulesManager
{
    class StringToInteger : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;


                if (string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(false, "");
                }

                if (int.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Vrednost mora biti u obliku celog pozitivnog broja.");
            }
            catch
            {
                return new ValidationResult(false, "Greška!");
            }
        }
    }
}
