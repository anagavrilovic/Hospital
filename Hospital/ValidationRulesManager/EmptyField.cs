using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidationRulesManager
{
    class EmptyField : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
              
                if (!string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "");
            }
            catch
            {
                return new ValidationResult(false, "Greška!");
            }
        }
    }
}
