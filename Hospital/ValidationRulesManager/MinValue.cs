using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidationRulesManager
{
    public class MinValue : ValidationRule
    {
        public double Min
        {
            get;
            set;
        }

     
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            if (value is double)
            {
                double d = (double)value;
                if (d < Min) return new ValidationResult(false, "Vrednost ne može biti negativna.");
                    return new ValidationResult(true, null);
            }
            if (value is int)
            {
                int d = (int)value;
                if (d < Min) return new ValidationResult(false, "Vrednost ne može biti negativna.");
                    return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Greška!");
            }
        }
    }
}
