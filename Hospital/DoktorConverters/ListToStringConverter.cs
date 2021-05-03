using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hospital.DoktorConverters
{
    class ListToStringConverter : IValueConverter
    {
        private List<string> names=new List<string>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            foreach(Ingredient ing in (System.Collections.Generic.List<Ingredient>)value)
            {
                names.Add(ing.Name);
            }
            return String.Join(", ", ((List<string>)names).ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
