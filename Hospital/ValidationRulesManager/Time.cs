using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.ValidationRulesManager
{
    class Time : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;


                if (string.IsNullOrEmpty(s))
                {
                    return new ValidationResult(false, "");
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$"))
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Morate uneti vreme u obliku HH:MM.");
                }       
            }
            catch
            {
                return new ValidationResult(false, "Greška!");
            }
        }
    }
}
